namespace turno_smart.ViewModels.EspecialidadVM

{
    public class ListEspecialidadVM
    {
        public List<EspecialidadVM> Especialidades { get; set; } = [];

        public string? Filter { get; set; }
    }
}