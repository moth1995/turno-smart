namespace turno_smart.ViewModels.EspecialidadVM
{
    public class EspecialidadVM
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public List<string> Profesionales { get; set; } = new List<string>();
        public string Categoria { get; set; }
    }

    public class ListaEspecialidadVM
    {
        public List<EspecialidadVM> Especialidades { get; set; } = new List<EspecialidadVM>();
        public string SearchTerm { get; set; }
        public string EspecialidadSeleccionada { get; set; }
    }
}
