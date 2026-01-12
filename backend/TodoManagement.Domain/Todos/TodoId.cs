namespace TodoManagement.Domain.Todos;
// value-object
//Record ne sağlar?
//1)Equals() otomatik
//2)GetHashCode() otomatik
// 3)== ve != otomatik
//4)Immutable kullanım teşvik edilir
//Value Object için olmazsa olmaz.

//Struct ne sağlar?
// value type. değerin kopyası gider. referans type değildir.

public readonly record struct TodoId(Guid Value)
{
    public static TodoId New() => new(Guid.NewGuid());
    public override string ToString() => Value.ToString();
}