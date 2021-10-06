namespace Template.Tests
{
    public abstract class UnitTestBase<TSut> where TSut : class
    {
        public UnitTestBase()
        {
            CultureInfo.CurrentCulture = new CultureInfo("en-AU");
            Mocker = new AutoMocker(Moq.MockBehavior.Default, Moq.DefaultValue.Mock);
            Sut = Mocker.CreateInstance<TSut>();
        }

        public AutoMocker Mocker { get; }

        public TSut Sut { get; }
    }
}
