using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.Linq;
using System.Web;

namespace Servo_API.App_Start
{
    public class DbOperations_LATEST
    {
        public class PrintingUtility
        {
            public static string printstr = "";
            private PrintDocument pd;
            public PrintingUtility()
            {
                pd = new PrintDocument();
                pd.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.print);
            }
            void print(object sender, PrintPageEventArgs e)
            {
            }
        }

        public delegate void CleanUpOperationHandler(object sender, EventArgs e);

        public enum OprType
        {
            Select, Insert, Update, Delete
        };

        public class DBUtil : IDisposable
        {
            protected static string constr;
            protected static System.Data.SqlClient.SqlConnection con;
            private bool IsCleanerAttached = false;
            public event CleanUpOperationHandler OnDispose = null;

            //==========FIRST CONSTRUCTOR==============
            public DBUtil()
            {
                constr = System.Configuration.ConfigurationSettings.AppSettings["Servosms"];
                //constr="Data Source=172.16.2.201;Initial Catalog=Matrimony; User ID=intranet; Password=intranet; max pool size=300";
            }
            //==========SECOND CONSTRUCTOR==============
            public DBUtil(string ConnectionString, bool AttachCleaner)
            {
                constr = ConnectionString;
                if (AttachCleaner)
                {
                    this.IsCleanerAttached = AttachCleaner;
                    this.OnDispose += new CleanUpOperationHandler(this.GC);
                }
            }

            public bool CleanerAttached
            {
                get
                {
                    return this.IsCleanerAttached;
                }
            }

            public static string ShowMessage(string message)
            {
                return "<script language='javascript'>alert('" + message + "')</script>;";
            }

            protected void GC(object sender, EventArgs e)
            {
                if (con != null)
                {
                    con.Close();
                    con.Dispose();
                    con = null;
                }
            }

            public void Dispose()
            {
                this.Dispose(true);
                System.GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool disposing)
            {
                if (disposing)
                {
                    if (IsCleanerAttached)
                        OnDispose(this, new EventArgs());
                    else
                    {
                        if (con != null)
                        {
                            con.Close();
                            con.Dispose();
                            con = null;
                        }
                    }
                }
            }

            public string ConnectionString
            {
                get
                {
                    return constr;
                }
                set
                {
                    constr = value;
                }
            }

            private void CloseConnection()
            {
            }

            private static System.Data.SqlClient.SqlCommand getCmd(string query)
            {
                con = new System.Data.SqlClient.SqlConnection(constr);
                con.Open();
                return new System.Data.SqlClient.SqlCommand(query, con);
            }

            /// <summary>
            /// Execute the SQL Select Query passed as parameter and returns the Output into the ref string parameter.
            /// </summary>
            public void SelectQuery(string query, string str, ref string outputvariable)
            {
                SqlDataReader dr;
                dr = getCmd(query).ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                if (dr.Read())
                {
                    outputvariable = dr[str].ToString();
                    if (OnDispose != null)
                        OnDispose(this, new EventArgs());
                }
                else
                    outputvariable = "";
            }

            /// <summary>
            /// Execute the Query Passed as a Parameter and returns the Output as ref parameter type SqlDataReader
            /// </summary>
            public void SelectQuery(string query, ref System.Data.SqlClient.SqlDataReader reader)
            {
                reader = getCmd(query).ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            }

            /// <summary>
            /// This Method Execute the passing Query as a parameter, Query of type Insert, 
            /// Update or Delete and returns the No. of Rows affected as a int in ref parameter 
            /// </summary>
            public void ExecuteScalar(string query, ref int outputvariable)
            {
                outputvariable = 0;
                object a = null;
                a = getCmd(query).ExecuteScalar();
                if (a != null)
                    outputvariable = System.Convert.ToInt32(a);

                if (OnDispose != null)
                    OnDispose(this, new EventArgs());
            }


            /// <summary>
            /// Execute the Procedure of passing name as a parameter procedurename followed by ref Object to contain 
            /// the Output, then the List of procedure parameters i.e. parameter name followed by values
            /// The Procedure type present in DBOperation.OprType , The Types are followings:
            /// Select, Insert, Delete and Update
            /// </summary>
            public void ExecProc(DbOperations_LATEST.OprType opr, string procedurename, ref object outputvariable, params object[] args)
            {
                SqlDataAdapter custDA = new SqlDataAdapter();
                //IF IT IS A SELECT QUERY==================
                //if (4) 
                if (opr.Equals(OprType.Select))
                {
                    System.Data.DataSet ds = new System.Data.DataSet();
                    System.Data.SqlClient.SqlCommand command = getCmd(procedurename);//.CommandType=System.Data.CommandType.StoredProcedure;
                    System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(command);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    if (args == null)
                        goto there;
                    for (int i = 0; i < args.Length; i++)
                    {
                        command.Parameters.Add(new System.Data.SqlClient.SqlParameter(args[i].ToString(), args[i + 1]));
                        ++i;
                    }
                    there:
                    da.Fill(ds);

                    outputvariable = ds.Tables[0].DefaultView;
                    if (OnDispose != null)
                        OnDispose(this, new EventArgs());
                }
                else if (opr.Equals(OprType.Insert))
                {
                    System.Data.SqlClient.SqlCommand command = getCmd(procedurename);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandTimeout = 180;
                    if (args == null)
                        goto there;
                    for (int i = 0; i < args.Length; i++)
                    {
                        command.Parameters.Add(new System.Data.SqlClient.SqlParameter(args[i].ToString(), args[i + 1]));
                        ++i;
                    }
                    there:
                    outputvariable = command.ExecuteNonQuery();
                    if (OnDispose != null)
                        OnDispose(this, new EventArgs());
                }
                else if (opr.Equals(OprType.Delete) || opr.Equals(OprType.Update))
                {
                    System.Data.SqlClient.SqlCommand command = getCmd(procedurename);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandTimeout = 180;

                    if (args == null)
                        goto there;
                    for (int i = 0; i < args.Length; i++)
                    {
                        command.Parameters.Add(new System.Data.SqlClient.SqlParameter(args[i].ToString(), args[i + 1]));
                        ++i;
                    }
                    there:
                    outputvariable = command.ExecuteNonQuery();
                    if (OnDispose != null)
                        OnDispose(this, new EventArgs());
                }
                else
                    outputvariable = new System.Data.DataSet();
            }

            /// <summary>
            /// Execute the Query of Type Insert/ Update or Delete passed as a parameter then returns the No. of Rows 
            /// Affected in ref output int parameter
            /// </summary>
            public void Insert_or_Update(string query, ref int outputvariable)
            {
                outputvariable = getCmd(query).ExecuteNonQuery();
                if (OnDispose != null)
                    OnDispose(this, new EventArgs());
            }

            /// <summary>
            /// Destructor
            /// </summary>
            ~DBUtil()
            {
                Dispose(false);
            }
        }
    }
}