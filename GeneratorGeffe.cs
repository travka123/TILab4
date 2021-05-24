namespace TILab4 {

    public class GeneratorGeffe {
        private LFSR lfsr1;
        private LFSR lfsr2;
        private LFSR lfsr3;

        public GeneratorGeffe(LFSR lfsr1, LFSR lfsr2, LFSR lfsr3) {
            this.lfsr1 = lfsr1;
            this.lfsr2 = lfsr2;
            this.lfsr3 = lfsr3;
        }

        public byte Next() {

            byte controlValue = lfsr1.Next();

            return (byte)((controlValue & lfsr2.Next()) | ((~controlValue) & lfsr3.Next()));
        }
    }
}
