using System;
using System.IO;
using Xunit;

namespace CProjekts.Test
{
    public class UnitTest1
    {
       
        [Theory]
        [InlineData(-5, new double[] { 0, 0 })]
        [InlineData(-1, new double[] { 0, 0 })]
        [InlineData(-9, new double[] { 0, 0 })]
        public void Fun_XIsLessThan0_ThenReturn0(double input1, double[] input2) // Metode_When_Then
        {
            // arrange
            var func = new Functions();


            //act
            double funcResponse = func.Fun(input1, input2);

            // assert
            Assert.Equal(0, funcResponse);
        }

        [Theory]
        [InlineData(@"\TestFiles\Example1", new double[] { 0, 1 }, new double[] { 0, 1 }, new double[] { 0, 1 })]
        [InlineData(@"\TestFiles\Example2", new double[] { 0, 1,2 }, new double[] { 1,1.1, 1.2 }, new double[] { 2, 2.1,2.2})]
        [InlineData(@"\TestFiles\Example3", new double[] { 0, 1,2 }, new double[] {10,50,100 }, new double[] { 1, 1,1 })]
        public void ConvertData_DataAreCorrect_ThenReturnData(string file, double[] X, double[] Y, double[] Z)
        {
            // arrange
            var func = new Functions();
            string FilePath = string.Format(Directory.GetCurrentDirectory()+ $"{file}.txt");
            string[] Data = File.ReadAllLines(FilePath);

            //act
            (double[] XR, double[] YR, double[] ZR) = func.ConvertData(Data);

            // assert
            Assert.Equal(X,XR);
            Assert.Equal(Y,YR);
            Assert.Equal(Z,ZR);
        }
        
    }
}
