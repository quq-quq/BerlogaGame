namespace InterfaceNode
{
    public interface ISleeper
    {
        public void Sleep(object caller);
        public void Sleep(float t, object caller);

        public void WakeUp(object caller);
    }
}
