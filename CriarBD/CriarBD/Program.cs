using System.Reflection;
using static System.Console;
using System.Data.SqlClient;
using System.Data;


//CreateTable();
access<Client> access = new access<Client>();

access.Update();



// GERA A STRING DE CRIAÇÃO DE TABELAS
static void CreateTable()
{
    string sql = "";

    // COLOCA EM UMA VARIAVEL A ASSEMBLY DO PROJETO
    var assembly = Assembly.GetExecutingAssembly();


    // PERCORRE TODOS OS TIPOS DA MINHA ASSEMBLY
    foreach (var type in assembly.GetTypes())
    {

        // SEPARA AQUILO QUE TEM A TAG TABLE
        if (type.GetCustomAttribute<TableAttribute>() != null)
        {
            // INICIA A STRING COM NOME DA TABELA
            sql = $"CREATE TABLE {type.Name} (";


            // PERCORRE TODAS AS PROPRIEDADES DA MINHA CLASSE DE TABELA
            foreach (var property in type.GetProperties())
            {


                // CONCATENA O NOME DAS COLUNAS
                sql += $" {property.Name}";


                // CONCATENA OS TIPOS DOS ATRIBUTOS
                if (property.GetCustomAttribute<IntAttribute>() != null)
                {
                    sql += " INT";

                }
                else if (property.GetCustomAttribute<VarCharAttribute>() != null)
                {
                    var typeatt = property.GetCustomAttribute<VarCharAttribute>();

                    sql += $" VARCHAR({typeatt.Size})";
                }

                // CONCATENA IDENTIDADE
                var identityatt = property.GetCustomAttribute<IdentityAttribute>();
                if (identityatt != null)
                {
                    sql += $" IDENTITY({identityatt.Initial},{identityatt.Increment})";
                }

                // CONCATENA NOT NULL
                var notNullatt = property.GetCustomAttribute<NotNullAttribute>();
                if (notNullatt != null)
                {
                    sql += " NOT NULL";
                }

                // CONCATENA PRIMARY KEY
                var primaryKeyatt = property.GetCustomAttribute<PrimaryKeyAttribute>();
                if (primaryKeyatt != null)
                {
                    sql += " PRIMARY KEY";
                }


                sql += ",";

            }


            // RETIRA A ULTIMA "," E ADICIONA ")"
            sql = sql.Substring(0, sql.Length - 1);
            sql += ")";

            WriteLine(sql);
            Banco banco = new Banco();
            banco.SendCommand(sql);
        }
    }
}


// METODOS DE BANCO DE DADOS
public class Banco
{
    // GERA A STRING DE DELETAR TABELAS
    public void DropTable()
    {
        string sql = "drop table Client";
        Banco banco = new Banco();
        banco.SendCommand(sql);
    }

    // ENVIA A STRING DE COMANDO PARA O SQL
    public void SendCommand(string sql)
    {
        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
        builder.InitialCatalog = "BD_Experimental";
        builder.DataSource = "JVLPC0553";
        builder.IntegratedSecurity = true;

        SqlConnection con;
        SqlCommand cmd;

        con = new SqlConnection(builder.ConnectionString);

        con.Open();

        cmd = new SqlCommand(sql, con);
        cmd.ExecuteNonQuery();

        con.Close();
    }
}


// CONFIGURA AS TAGS (TABLE, PRIMARY KEY, VARCHAR ...)
public class TableAttribute : Attribute
{

}
public class PrimaryKeyAttribute : Attribute
{

}
public class IdentityAttribute : Attribute
{
    public IdentityAttribute(int initial, int increment)
    {
        Initial = initial;
        Increment = increment;
    }

    public IdentityAttribute() { }

    public int Initial { get; set; } = 1;
    public int Increment { get; set; } = 1;
}
public class NotNullAttribute : Attribute
{

}
public class IntAttribute : Attribute
{

}
public class VarCharAttribute : Attribute
{
    public VarCharAttribute(int size)
    {
        Size = size;
    }

    public VarCharAttribute() { }

    public int Size { get; set; } = 100;
}


// DEFINE AS TABELAS 
[Table]
public class Client
{
    [Int]
    [PrimaryKey]
    [Identity()]
    public int Id_Client { get; set; }

    [VarChar(50)]
    [NotNull]
    public string Name { get; set; }
}
[Table]
public class Product
{
    [Int]
    [PrimaryKey]
    [Identity()]
    public int Id_Product { get; set; }

    [VarChar(100)]
    [NotNull]
    public string Name { get; set; }
}
[Table]
public class Endereco
{
    [Int]
    [PrimaryKey]
    [Identity()]
    public int Id_Endereco { get; set; }

    [VarChar(100)]
    [NotNull]
    public string Rua { get; set; }

    [Int]
    [NotNull]
    public string Numero { get; set; }
}





public class access<T>
    where T : new()
{

    public void Select()
    {

        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
        builder.InitialCatalog = "BD_Experimental";
        builder.DataSource = "JVLPC0553";
        builder.IntegratedSecurity = true;

        SqlConnection con;
        SqlCommand cmd;

        con = new SqlConnection(builder.ConnectionString);

        con.Open();

        var query = "select * from " + typeof(T).Name;

        cmd = new SqlCommand(query, con);

        DataTable dt = new DataTable();
        dt.Load(cmd.ExecuteReader());

        con.Close();

        foreach (DataRow dataRow in dt.Rows)
        {
            foreach (var item in dataRow.ItemArray)
            {
                Console.Write($"{item} ");
            }
            WriteLine();
        }
        
    }
    
    public void SelectWhereId()
    {
        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
        builder.InitialCatalog = "BD_Experimental";
        builder.DataSource = "JVLPC0553";
        builder.IntegratedSecurity = true;

        SqlConnection con;
        SqlCommand cmd;

        con = new SqlConnection(builder.ConnectionString);

        con.Open();


        var assembly = Assembly.GetExecutingAssembly();

        string id = "";
        foreach (var type in assembly.GetTypes())
        {
            if (type.Name == typeof(T).Name)
            {
                foreach (var property in type.GetProperties())
                {
                    if (property.GetCustomAttribute<PrimaryKeyAttribute>() != null)
                    {
                        id = property.Name;
                    }
                }
            }
        }



        var query = "select * from " + typeof(T).Name + $" WHERE {id} = ";

        Write("Digite a id que deseja procurar: ");
        query += $"{ReadLine()}";


        cmd = new SqlCommand(query, con);

        DataTable dt = new DataTable();
        dt.Load(cmd.ExecuteReader());

        con.Close();

        foreach (DataRow dataRow in dt.Rows)
        {
            foreach (var item in dataRow.ItemArray)
            {
                Console.Write($"{item} ");
            }
            WriteLine();
        }
    }

    public void SelectWhere()
    {
        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
        builder.InitialCatalog = "BD_Experimental";
        builder.DataSource = "JVLPC0553";
        builder.IntegratedSecurity = true;

        SqlConnection con;
        SqlCommand cmd;

        con = new SqlConnection(builder.ConnectionString);

        con.Open();

        int i = 0;
        var assembly = Assembly.GetExecutingAssembly();

        WriteLine("Menu\n");
        foreach (var type in assembly.GetTypes())
        {
            if (type.Name == typeof(T).Name)
            {
                foreach (var property in type.GetProperties())
                {
                    WriteLine($"{i} - {property.Name}");
                    i++;
                }
            }
        }


        string campo = "";
        int valor = int.Parse(ReadLine());
        i = 0;

        foreach (var type in assembly.GetTypes())
        {
            if (type.Name == typeof(T).Name)
            {
                foreach (var property in type.GetProperties())
                {
                    if(i == valor)
                    {
                        campo = property.Name;
                    }
                    i++;
                }
            }
        }

  
        var query = "select * from " + typeof(T).Name + $" WHERE {campo} = ";

        Write($"Digite a {campo} que deseja procurar: ");
        query += $"'{ReadLine()}'";


        cmd = new SqlCommand(query, con);

        DataTable dt = new DataTable();
        dt.Load(cmd.ExecuteReader());

        con.Close();

        foreach (DataRow dataRow in dt.Rows)
        {
            foreach (var item in dataRow.ItemArray)
            {
                Console.Write($"{item} ");
            }
            WriteLine();
        }
    }

    public void Insert()
    {

        var assembly = Assembly.GetExecutingAssembly();

        foreach (var type in assembly.GetTypes())
        {
            if (type.Name == typeof(T).Name)
            {

                string comando = $"INSERT INTO {type.Name} (";


                // PERCORRE TODAS AS PROPRIEDADES DA MINHA CLASSE DE TABELA
                foreach (var property in type.GetProperties())
                {
                    if (property.GetCustomAttribute<PrimaryKeyAttribute>() == null)
                    {
                        comando += $"{property.Name},";
                    }
                }

                comando = comando.Substring(0, comando.Length - 1);
                comando += ") VALUES (";

                foreach (var property in type.GetProperties())
                {
                    if (property.GetCustomAttribute<PrimaryKeyAttribute>() == null)
                    {
                        WriteLine($"Digite o valor para {property.Name}");

                        comando += $"'{ReadLine()}',";
                    }
                }
                comando = comando.Substring(0, comando.Length - 1);
                comando += ")";

                Banco banco = new Banco();
                banco.SendCommand(comando);
            }
        }
    }

    public void Delete()
    {
        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
        builder.InitialCatalog = "BD_Experimental";
        builder.DataSource = "JVLPC0553";
        builder.IntegratedSecurity = true;

        SqlConnection con;
        SqlCommand cmd;

        con = new SqlConnection(builder.ConnectionString);

        con.Open();

        int i = 0;
        var assembly = Assembly.GetExecutingAssembly();

        WriteLine("Menu\n");
        foreach (var type in assembly.GetTypes())
        {
            if (type.Name == typeof(T).Name)
            {
                foreach (var property in type.GetProperties())
                {
                    WriteLine($"{i} - {property.Name}");
                    i++;
                }
            }
        }


        string campo = "";
        int valor = int.Parse(ReadLine());
        i = 0;

        foreach (var type in assembly.GetTypes())
        {
            if (type.Name == typeof(T).Name)
            {
                foreach (var property in type.GetProperties())
                {
                    if (i == valor)
                    {
                        campo = property.Name;
                    }
                    i++;
                }
            }
        }


        var query = "DELETE from " + typeof(T).Name + $" WHERE {campo} = ";

        Write($"Digite a {campo} que deseja procurar: ");
        query += $"'{ReadLine()}'";


        cmd = new SqlCommand(query, con);

        DataTable dt = new DataTable();
        dt.Load(cmd.ExecuteReader());

        con.Close();

        foreach (DataRow dataRow in dt.Rows)
        {
            foreach (var item in dataRow.ItemArray)
            {
                Console.Write($"{item} ");
            }
            WriteLine();
        }
    }

    public void Update()
    {
        int i = 0;
        var assembly = Assembly.GetExecutingAssembly();

        WriteLine("Menu\n");
        foreach (var type in assembly.GetTypes())
        {
            if (type.Name == typeof(T).Name)
            {
                foreach (var property in type.GetProperties())
                {
                    WriteLine($"{i} - {property.Name}");
                    i++;
                }
            }
        }


        string campo = "";
        int valor = int.Parse(ReadLine());
        i = 0;

        foreach (var type in assembly.GetTypes())
        {
            if (type.Name == typeof(T).Name)
            {
                foreach (var property in type.GetProperties())
                {
                    if (i == valor)
                    {
                        campo = property.Name;
                    }
                    i++;
                }
            }
        }


            

        
        Write($"Digite o valor do campo que quer atualizar: ");
        string linha = $"'{ReadLine()}'";
        Write($"Digite o novo valor desse campo: ");
        string novo = $"'{ReadLine()}'";


        var query = "UPDATE " + typeof(T).Name + " SET " + campo + " = " + novo + " where " + campo + " = " + linha;

   

        Banco banco = new Banco();
        banco.SendCommand(query);

    }
        

    
}
