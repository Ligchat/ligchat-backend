public class ContactViewModelResult
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public int? TagId { get; set; }
    public string Numero { get; set; }
    public string FotoPerfil { get; set; }
    public string Email { get; set; }
    public string Anotacoes { get; set; }
    public string Status { get; set; }
    public int SetorId { get; set; }

    public ContactViewModelResult(int id, string nome, int? tagId, string numero, string fotoPerfil, string email, string anotacoes, string status, int setorId)
    {
        Id = id;
        Nome = nome;
        TagId = tagId;
        Numero = numero;
        FotoPerfil = fotoPerfil;
        Email = email;
        Anotacoes = anotacoes;
        Status = status;
        SetorId = setorId;
    }
} 