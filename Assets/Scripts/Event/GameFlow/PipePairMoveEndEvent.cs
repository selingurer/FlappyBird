namespace DefaultNamespace
{
    public struct PipePairMoveEndEvent
    {
        public PipePair Pair;
        public PipePairMoveEndEvent(PipePair pair)
        {
            Pair = pair;
        }
    }
}