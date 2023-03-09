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
            var mixedcase50 = TextToNumberConverter.Convert("eLl�").Output;
            var uppercase50 = TextToNumberConverter.Convert("ELL�").Output;
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
            UserOutputModel userOutputModel = TextToNumberConverter.Convert("y�z");
            Assert.Equal("100", userOutputModel.Output);
        }

        [Fact]
        public void SampleTest_1_024()
        {
            Assert.Equal(1024.ToString("N0") + " lira eksi�im kald�", TextToNumberConverter.Convert("Bin 20 d�rt lira eksi�im kald�").Output);
            Assert.Equal(1024.ToString("N0") + " lira eksi�im kald�", TextToNumberConverter.Convert("Bin yirmi d�rt lira eksi�im kald�").Output);
        }

        [Fact]
        public void SampleTest_100_000()
        {
            Assert.Equal(100000.ToString("N0") + " lira kredi kullanmak istiyorum", TextToNumberConverter.Convert("100 bin lira kredi kullanmak istiyorum").Output);
            Assert.Equal(100000.ToString("N0") + " lira kredi kullanmak istiyorum", TextToNumberConverter.Convert("Y�z bin lira kredi kullanmak istiyorum").Output);
        }

        [Fact]
        public void SampleTest_111_111_111_111()
        {
            Assert.Equal(111111111111.ToString("N0"), TextToNumberConverter.Convert("y�zonbirmilyary�zonbirmilyony�zonbirbiny�zonbir").Output);
            Assert.Equal(111111111111.ToString("N0"), TextToNumberConverter.Convert("y�z onbirmilyary�zon birmilyony�zonbir biny�zonbir").Output);
        }

        [Fact]
        public void SampleTest_16()
        {
            Assert.Equal("Yar�n saat 16da gelece�im", TextToNumberConverter.Convert("Yar�n saat 10alt�da gelece�im").Output);
            Assert.Equal("Yar�n saat 16da gelece�im", TextToNumberConverter.Convert("Yar�n saat onalt�da gelece�im").Output);
        }

        [Fact]
        public void SampleTest_28()
        {
            Assert.Equal("Bug�n 28 ya��na girdim", TextToNumberConverter.Convert("Bug�n yirmi 8 ya��na girdim").Output);
            Assert.Equal("Bug�n 28 ya��na girdim", TextToNumberConverter.Convert("Bug�n yirmi sekiz ya��na girdim").Output);
        }

        [Fact]
        public void SampleTest_56_000()
        {
            Assert.Equal(56000.ToString("N0") + " lira kredi al�p 3 y�lda geri �deyece�im", TextToNumberConverter.Convert("Elli 6 bin lira kredi al�p �� y�lda geri �deyece�im").Output);
            Assert.Equal(56000.ToString("N0") + " lira kredi al�p 3 y�lda geri �deyece�im", TextToNumberConverter.Convert("Elli alt� bin lira kredi al�p �� y�lda geri �deyece�im").Output);
        }

        [Fact]
        public void SampleTest_87_216()
        {
            Assert.Equal(87216.ToString("N0") + " lira borcum var", TextToNumberConverter.Convert("Seksen 7 bin iki 100 on alt� lira borcum var").Output);
            Assert.Equal(87216.ToString("N0") + " lira borcum var", TextToNumberConverter.Convert("Seksen yedi bin iki y�z on alt� lira borcum var").Output);
        }

        [Fact]
        public void SampleTest_955()
        {
            Assert.Equal("955 lira fiyat� var", TextToNumberConverter.Convert("9y�z50be� lira fiyat� var").Output);
            Assert.Equal("955 lira fiyat� var", TextToNumberConverter.Convert("Dokuzy�zelli be� lira fiyat� var").Output);
        }
    }
}