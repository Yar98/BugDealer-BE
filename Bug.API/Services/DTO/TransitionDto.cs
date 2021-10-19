namespace Bug.Entities.Dtos
{
	public class TransitionDto
	{
		public string Id { get; set; }
		public string WorkflowId { get; set; }
		public string StartStatusId { get; set; }
		public string EndStatusId { get; set; }
		//#pragma warning restore CS8632 // The annotation for null able reference types should only be used in code within a '#nullable' annotations context.

	}
}