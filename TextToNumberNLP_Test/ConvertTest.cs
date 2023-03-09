using TextToNumberNLP;
using TextToNumberNLP.Models;
using Xunit;

namespace TextToNumberNLP_Test
{
    public class ConvertTest
    {
        [Fact]
        public void ConvertsCaseInsensitive()
        {
            var lowercase50 = TextToNumberConverter.Convert("elli").Output;
            var mixedcase50 = TextToNumberConverter.Convert("eLlÝ").Output;
            var uppercase50 = TextToNumberConverter.Convert("ELLÝ").Output;
            Assert.Equal("50", lowercase50);
            Assert.Equal("50", mixedcase50);
            Assert.Equal("50", uppercase50);
        }

        [Fact]
        public void FailSafely_InputNullEmptyWhitespace()
        {
            var outputFromNull = TextToNumberConverter.Convert(null).Output;
            var outputFromEmpty = TextToNumberConverter.Convert("").Output;
            var outputFromWhitespace = TextToNumberConverter.Convert(" \t \n  \r ").Output;
            Assert.Equal(String.Empty, outputFromNull);
            Assert.Equal(String.Empty, outputFromEmpty);
            Assert.Equal(String.Empty, outputFromWhitespace);
        }

        [Fact]
        public void Hundred_ConvertsAs_100()
        {
            UserOutputModel userOutputModel = TextToNumberConverter.Convert("yüz");
            Assert.Equal("100", userOutputModel.Output);
        }

        [Fact]
        public void SampleTest_1_024()
        {
            Assert.Equal(1024.ToString("N0") + " lira eksiðim kaldý", TextToNumberConverter.Convert("Bin 20 dört lira eksiðim kaldý").Output);
            Assert.Equal(1024.ToString("N0") + " lira eksiðim kaldý", TextToNumberConverter.Convert("Bin yirmi dört lira eksiðim kaldý").Output);
        }

        [Fact]
        public void SampleTest_100_000()
        {
            Assert.Equal(100000.ToString("N0") + " lira kredi kullanmak istiyorum", TextToNumberConverter.Convert("100 bin lira kredi kullanmak istiyorum").Output);
            Assert.Equal(100000.ToString("N0") + " lira kredi kullanmak istiyorum", TextToNumberConverter.Convert("Yüz bin lira kredi kullanmak istiyorum").Output);
        }

        [Fact]
        public void SampleTest_111_111_111_111()
        {
            Assert.Equal(111111111111.ToString("N0"), TextToNumberConverter.Convert("yüzonbirmilyaryüzonbirmilyonyüzonbirbinyüzonbir").Output);
            Assert.Equal(111111111111.ToString("N0"), TextToNumberConverter.Convert("yüz onbirmilyaryüzon birmilyonyüzonbir binyüzonbir").Output);
        }

        [Fact]
        public void SampleTest_16()
        {
            Assert.Equal("Yarýn saat 16da geleceðim", TextToNumberConverter.Convert("Yarýn saat 10altýda geleceðim").Output);
            Assert.Equal("Yarýn saat 16da geleceðim", TextToNumberConverter.Convert("Yarýn saat onaltýda geleceðim").Output);
        }

        [Fact]
        public void SampleTest_28()
        {
            Assert.Equal("Bugün 28 yaþýna girdim", TextToNumberConverter.Convert("Bugün yirmi 8 yaþýna girdim").Output);
            Assert.Equal("Bugün 28 yaþýna girdim", TextToNumberConverter.Convert("Bugün yirmi sekiz yaþýna girdim").Output);
        }

        [Fact]
        public void SampleTest_56_000()
        {
            Assert.Equal(56000.ToString("N0") + " lira kredi alýp 3 yýlda geri ödeyeceðim", TextToNumberConverter.Convert("Elli 6 bin lira kredi alýp üç yýlda geri ödeyeceðim").Output);
            Assert.Equal(56000.ToString("N0") + " lira kredi alýp 3 yýlda geri ödeyeceðim", TextToNumberConverter.Convert("Elli altý bin lira kredi alýp üç yýlda geri ödeyeceðim").Output);
        }

        [Fact]
        public void SampleTest_87_216()
        {
            Assert.Equal(87216.ToString("N0") + " lira borcum var", TextToNumberConverter.Convert("Seksen 7 bin iki 100 on altý lira borcum var").Output);
            Assert.Equal(87216.ToString("N0") + " lira borcum var", TextToNumberConverter.Convert("Seksen yedi bin iki yüz on altý lira borcum var").Output);
        }

        [Fact]
        public void SampleTest_955()
        {
            Assert.Equal("955 lira fiyatý var", TextToNumberConverter.Convert("9yüz50beþ lira fiyatý var").Output);
            Assert.Equal("955 lira fiyatý var", TextToNumberConverter.Convert("Dokuzyüzelli beþ lira fiyatý var").Output);
        }
    }
}