using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace CProjekts.Test
{
    public class UnitTest1
    {
        public class ResponseFunctionTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { -5, new List<double> { 0, 0,0,0,0,0 } };
                yield return new object[] { -1, new List<double> { 0, 0, 0, 0, 0, 0 } };
                yield return new object[] { -9, new List<double> { 0, 0, 0, 0, 0, 0 } };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        [Theory]
        [ClassData(typeof(ResponseFunctionTestData))]
        public void Fun_XIsLessThan0_ThenReturn0(double input1, List<double> input2) // Metode_When_Then
        {
            // arrange
            var functions = new ResponseFunction();


            //act
           var functionResponse = functions.Value(input2, input1);

            // assert
            Assert.Equal(0, functionResponse);
        }

        public class ResponseFunctionTestDataV2 : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { -5, new List<double> { 0, 0, 0, 0, 0, 0 }, "Time constant cannot be 0" };                
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        [Theory]
        [ClassData(typeof(ResponseFunctionTestDataV2))]
        public void Fun_TimeIsEqualToZero_ThenReturnError(double input1, List<double> input2, string error) // Metode_When_Then
        {
            // arrange
            var functions = new ResponseFunction();


            //act
            try { functions.Value(input2, input1); }
            // assert
            catch (Exception ex) { Assert.Equal(error, ex.Message); }
                       
        }

        [Theory]
        [InlineData(@"\TestFiles\Example1_1", new double[] { 0, 1 }, new double[] { 0, 1 }, new double[] { 0, 1 })]
        [InlineData(@"\TestFiles\Example1_2", new double[] { 0, 1,2 }, new double[] { 1, 1.1, 1.2 }, new double[] { 2, 2.1, 2.2})]
        [InlineData(@"\TestFiles\Example1_3", new double[] { 0, 1,2 }, new double[] {10, 50, 100 }, new double[] { 1, 1, 1 })]
        public void ConvertData_DataAreCorrect_ThenReturnThreeColumnsOfData(string file, double[] X, double[] Y, double[] Z)
        {
            // arrange
            var functions = new Functions();
            string FilePath = string.Format(Directory.GetCurrentDirectory()+ $"{file}.txt");
            string[] Data = File.ReadAllLines(FilePath);

            //act
            (double[] XR, double[] YR, double[] ZR) = functions.ReadDataFromFile(Data);

            // assert
            Assert.Equal(X,XR);
            Assert.Equal(Y,YR);
            Assert.Equal(Z,ZR);
        }

        [Theory]
        [InlineData(@"\TestFiles\Example2_1", null, null, null)]
        [InlineData(@"\TestFiles\Example2_2", null, null, null)]
        public void ConvertData_DataAreIncomplite_ThenReturnEmptyArrays(string file, double[] X, double[] Y, double[] Z)
        {
            // arrange
            var functions = new Functions();
            string FilePath = string.Format(Directory.GetCurrentDirectory() + $"{file}.txt");
            string[] Data = File.ReadAllLines(FilePath);

            //act
            (double[] XR, double[] YR, double[] ZR) = functions.ReadDataFromFile(Data);

            // assert
            Assert.Equal(X, XR);
            Assert.Equal(Y, YR);
            Assert.Equal(Z, ZR);
        }

    }
}
