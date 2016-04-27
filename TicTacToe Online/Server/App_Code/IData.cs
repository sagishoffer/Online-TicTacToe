// Sagi Shoffer & Oren Yulzary

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


[DataContract]
public class PlayerObject
{
    [DataMember]
    public int Id { get; set; }

    [DataMember]
    public string UserName { get; set; }

    [DataMember]
    public string FirstName { get; set; }

    [DataMember]
    public string LastName { get; set; }

    [DataMember]
    public DateTime CreatedAt { get; set; }

    [DataMember]
    public string Rank { get; set; }
}

[DataContract]
public class AdviserObject : PlayerObject
{
    [DataMember]
    public int AdviseId { get; set; }

    [DataMember]
    public string AdviseTo { get; set; }
}

[DataContract]
public class PlayersGamesObject : PlayerObject
{
    [DataMember]
    public int NumOfGames { get; set; }
}

[DataContract]
public class ChampsObject
{
    [DataMember]
    public int Id { get; set; }

    [DataMember]
    public string Name { get; set; }

    [DataMember]
    public string City { get; set; }

    [DataMember]
    public DateTime Date { get; set; }

    [DataMember]
    public string Image { get; set; }
}

[DataContract]
public class BoardsObject
{
    [DataMember]
    public int Id { get; set; }

    [DataMember]
    public string Name { get; set; }

    [DataMember]
    public int Size { get; set; }

    [DataMember]
    public char Mode { get; set; }

    [DataMember]
    public string Description { get; set; }
}

[DataContract]
public class GameObject
{
    [DataMember]
    public int Id { get; set; }

    [DataMember]
    public string BoardName { get; set; }

    [DataMember]
    public int BoardSize { get; set; }

    [DataMember]
    public History Steps { get; set; }

    [DataMember]
    public string Player1 { get; set; }

    [DataMember]
    public string Player2 { get; set; }

    [DataMember]
    public DateTime Date { get; set; }
}

[DataContract]
public class History
{
    [DataMember]
    public char starter { get; set; }

    [DataMember]
    public List<int> steps { get; set; }

    [DataMember]
    public char winner { get; set; }
}

[DataContract]
public class CityObject
{
    [DataMember]
    public string Name { get; set; }

    [DataMember]
    public int NumOfChamps { get; set; }
}