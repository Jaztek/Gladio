using MongoDB.Bson;

[System.Serializable]
public class WeaponMaestro
{
    public ObjectId Id { get; set; }
    public string identificador;
    public string sprite;
    public int damage;
    public int force;
    public int velocidad;
    public int tipoHabilidadBasica;

    // 1 corta, 2 media, 3 larga.
    public int alcance;

    // hace falta meter una variable de proximidad.
}