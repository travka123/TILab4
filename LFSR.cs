using System.Collections;

namespace TILab4 {
    public class LFSR {

        public static int AdditionalBufferSize { get; } = 256;

        private int startpos = 0;

        private int[] xorspos;

        private BitArray sequence;

        private int length;

        public LFSR(int[] xorspos, string initstr) {    

            length = initstr.Length;
            sequence = new BitArray(length + AdditionalBufferSize, false);
            for (int i = 0; i < length; i++) {
                if (initstr[i] == '1') {
                    sequence[i] = true;
                }
            }

            this.xorspos = xorspos;
            for (int i = 0; i < xorspos.Length; i++) {
                xorspos[i] = length - xorspos[i] - 1;
            }
        }

        public byte Next() {
            byte seqpart = 0;
            for (int i = 0; i < 8; i++) {
                seqpart = (byte)(seqpart << 1);
                if (ShiftSequence()) {
                    seqpart |= 1;
                }
            }
            return seqpart;
        }

        private bool ShiftSequence() {
            if (startpos + length == sequence.Length) {
                for (int i = 0; i < length; i++) {
                    sequence[i] = sequence[startpos + i];
                }
                startpos = 0;
            }

            bool leftBit = sequence[startpos];

            bool newRightBit = leftBit;
            foreach (int pos in xorspos) {
                newRightBit ^= sequence[startpos + pos];
            }
            sequence[startpos + length] = newRightBit;
            startpos++;

            return leftBit;
        }
    }
}
