using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;
using JamSoft.Helpers.Crypto;
using Xunit;
using Xunit.Abstractions;

namespace JamSoft.Helpers.Tests.Cryptography
{
    public class RsaCryptoTests
    {
        private readonly ITestOutputHelper _outputHelper;
        private readonly string _privateKey = "<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<RSAParameters xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\r\n  <Exponent>AQAB</Exponent>\r\n  <Modulus>nyo4kg83PebAPi/YyYXxdGDbf1M+ThEDySqYkOsBU9/Ubav5A1VbW1W/6DyWjiwTi+1me5XL62P2IbP+vI1OfsGF8n81qxlty2lxUWl6u1SDRPFPIEtV8S/jm3Gn989+UB/bWnI+K9Dk6iKABgS5FBsN1hO+VM0lgAZdiXSabGQFjn7c+2MAK3I5cvjpbK4aqjYMDpD4JtXZMlFwrY2ISz3cUBlelnv2aH4G5jYFJXtfgvta5CaEUXDkZMllFnWEsjtJbN6TVmpgHFgYRGl04NQ3YdrjuD+dWiIwcawymMXp1jmyc5fbajf6U7gu09b4D7LdBBh6Tew5lMjKNUsGnQ==</Modulus>\r\n  <P>zfBrXYnTcRsWOFuhgbCJYCdRZC0LjsQe0u0Dna07GAaGIuIF8x2MZzNkKuF/QUBmtnVdI1D56vg2NYa9qFTHRczSG3QSY3HzsWKG3bRFKyKW7j1RWceowcsnlGIGgITT6ZQKwKkbpD/aKoXzMymeu1UE2sM8eLyTfDvsJzMiJ7c=</P>\r\n  <Q>xdsLiJ2fRJ3JoCN9dI9STq1icaJD3hZZi0xfxRjoKY1+dxmE7Rrlhhc8w7gd5RVqxpVl3fyrwjLAPTtd2voS98qRx2eIaSlzM9JUimMczxz7AKn630ufJZwPEEvSj7YlBQt3dvmfRzOJ2s/AWsDfmHLrC6PxlFZLdXSroF3ZvEs=</Q>\r\n  <DP>L1IGKako172EnpCXjOhWuKxwLFeZZ0WzW34wrYOHp56gJdXPzixE/dW2N5A3IHQ+5cAUFbBerNo6ApSicdKBM4273akPLKCbgXAFU14/4oiBK98VGU8ifN1Ei/j2S4O5+dsVmW2CN3ygkdLTrjbrDVqc0fO4qnmSXiKawesi9wM=</DP>\r\n  <DQ>K5RCpxWotfFXLqmCgYDr7R5td3/5GNqtYGwzD/Obd0OOHmeFisAI3A8UODu+ge3EtfbEGDAGGOEazKHd21SHhwKcN2KLfjY+BKUIL+8Csm+8rXnDIxnB9QO5oapBt5uz7beH2bHDrmggrgxiXtrqpOZy9P5oQHb9aeKHxuvFAJ8=</DQ>\r\n  <InverseQ>c0GtENRAO8FhPOnsUEfI3nnrzr+1Cpm4JpDEVW3EzolbP0YOGU0/KDNBEaymJa/cOeIr9XfAtWC9ZC5TzU5Casqe8z6AaktiKURjR0oirv+J4YxbrnWxTWq6w+ZBahUc3FYpxacygMxPHhESgO09A4EcBL5KBoBIwGIhuwCkGbw=</InverseQ>\r\n  <D>Fzo2HzqROTtferPI0z/0yKMJ5T7krMfW4ZiRwzRIcVEM0yRxpobiWiXdZ6rP3deY0qbGeeqWY7emx6xY1HNarSzYu1bNIjcHytOMcfEOtB/VOE5u2auk0xnGAX1IoeVp7Y94l25snEBT58c5H6e4yrJYBpHDCOUXP+Ot6s1Va9PB/lA/ORasBqRN+cgosLXUfsgN2pVaGr7Zn1I+PTNJNpvojiZtUpPsWKtXTHZSrcOBBUJT8J7Exxy/UJpHhLWoXsZodXKKoegUG4waubeJsdbWdOw1AvYTW2v8bYt2THIJhyaniat0aXTtH9TVFriU4NQW4Tdk0WVSxH5SgF/8EQ==</D>\r\n</RSAParameters>";
        private readonly string _publicKey = "<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<RSAParameters xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\r\n  <Exponent>AQAB</Exponent>\r\n  <Modulus>nyo4kg83PebAPi/YyYXxdGDbf1M+ThEDySqYkOsBU9/Ubav5A1VbW1W/6DyWjiwTi+1me5XL62P2IbP+vI1OfsGF8n81qxlty2lxUWl6u1SDRPFPIEtV8S/jm3Gn989+UB/bWnI+K9Dk6iKABgS5FBsN1hO+VM0lgAZdiXSabGQFjn7c+2MAK3I5cvjpbK4aqjYMDpD4JtXZMlFwrY2ISz3cUBlelnv2aH4G5jYFJXtfgvta5CaEUXDkZMllFnWEsjtJbN6TVmpgHFgYRGl04NQ3YdrjuD+dWiIwcawymMXp1jmyc5fbajf6U7gu09b4D7LdBBh6Tew5lMjKNUsGnQ==</Modulus>\r\n</RSAParameters>";

        private readonly string _userData = "some-unique-thing";
        private readonly string _userDataAlt = "some-different-thing";

        public RsaCryptoTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        internal IRsaCrypto GetGenerator() => new RsaCryptoFactory().Create(_privateKey, null);
        internal IRsaCrypto GetVerifier() => new RsaCryptoFactory().Create(null, _publicKey);
        
        [Fact]
        public void Ctr_Factory_Default()
        {
            var sut = new RsaCryptoFactory().Create();
            Assert.NotNull(sut);
        }

        [Fact]
        public void Ctr_Factory_Default_PublicKey_Null()
        {
            var sut = new RsaCryptoFactory().Create();
            Assert.Null(sut.PublicKey);
        }

        [Fact]
        public void Ctr_Factory_Default_PrivateKey_Null()
        {
            var sut = new RsaCryptoFactory().Create();
            Assert.Null(sut.PrivateKey);
        }

        [Fact]
        public void Ctr_Factory_KeySize()
        {
            var sut = new RsaCryptoFactory().Create(512);
            _outputHelper.WriteLine($"KeySize: 512   Val: {sut.PrivateKey}");

            var sut2 = new RsaCryptoFactory().Create(1024);
            _outputHelper.WriteLine($"KeySize: 1024  Val: {sut2.PrivateKey}");
            Assert.NotNull(sut);
        }

        [Fact]
        public void Ctr_Factory_Xml_Keys()
        {
            var sut = new RsaCryptoFactory().Create(_privateKey, _publicKey);
            Assert.NotNull(sut);
            Assert.NotNull(sut.PublicKey);
            Assert.NotNull(sut.PrivateKey);
        }

        [Fact]
        public void Ctr_Factory_Xml_Keys_Hash_Padding()
        {
            var sut = new RsaCryptoFactory().Create(_privateKey, _publicKey, HashAlgorithmName.SHA256, RSASignaturePadding.Pss);
            Assert.NotNull(sut);
            Assert.NotNull(sut.PublicKey);
            Assert.NotNull(sut.PrivateKey);
        }

        [Fact]
        public void Ctr_Factory_Rsa()
        {
            var sut = new RsaCryptoFactory().Create(new RSACryptoServiceProvider(1024));
            Assert.NotNull(sut);
        }

        [Fact]
        public void Ctr_Factory_Rsa_Keys()
        {
            var sr1 = new StringReader(_privateKey);
            var sr2 = new StringReader((_publicKey));
            var xs = new XmlSerializer(typeof(RSAParameters));
            var privateKey = (RSAParameters)xs.Deserialize(sr1);
            var publicKey = (RSAParameters)xs.Deserialize(sr2);
            
            var sut = new RsaCryptoFactory().Create(privateKey, publicKey);
            Assert.NotNull(sut);
            Assert.NotNull(sut.PublicKey);
            Assert.NotNull(sut.PrivateKey);
        }

        [Fact]
        public void Ctr_Factory_Rsa_Keys_Hash_Padding()
        {
            var sr1 = new StringReader(_privateKey);
            var sr2 = new StringReader((_publicKey));
            var xs = new XmlSerializer(typeof(RSAParameters));
            var privateKey = (RSAParameters)xs.Deserialize(sr1);
            var publicKey = (RSAParameters)xs.Deserialize(sr2);

            var sut = new RsaCryptoFactory().Create(privateKey, publicKey, HashAlgorithmName.SHA256, RSASignaturePadding.Pss);
            Assert.NotNull(sut);
            Assert.NotNull(sut.PublicKey);
            Assert.NotNull(sut.PrivateKey);
        }

        [Fact]
        public void Ctr_Factory_KeySize_Hash_Padding()
        {
            var sut = new RsaCryptoFactory().Create(512, HashAlgorithmName.SHA256, RSASignaturePadding.Pss);
            Assert.NotNull(sut);
            Assert.Null(sut.PublicKey);
            Assert.Null(sut.PrivateKey);
        }

        [Fact]
        public void Ctr_Factory_Hash_Padding()
        {
            var sut = new RsaCryptoFactory().Create(HashAlgorithmName.SHA256, RSASignaturePadding.Pss);
            Assert.NotNull(sut);
            Assert.Null(sut.PublicKey);
            Assert.Null(sut.PrivateKey);
        }

        [Fact]
        public void Sign_Verify_Correct_Data_Success()
        {
            var generator = GetGenerator();

            var licenseText = Format(_userData, 1);

            var signed = generator.SignData(licenseText);

            Assert.NotNull(signed);

            var verifier = GetVerifier();

            var userLicense = Format(_userData, 1);
            var verified = verifier.VerifyData(userLicense, signed);

            Assert.True(verified);
        }

        [Fact]
        public void Sign_Verify_Correct_Data_Success_Sha256()
        {
            var generator = new RsaCryptoFactory().Create(HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

            var licenseText = Format(_userData, 1);

            var signed = generator.SignHash(licenseText);

            Assert.NotNull(signed);
            Assert.NotNull(generator.PublicKey);

            var publicKeyXml = generator.PublicKey;

            var verifier = new RsaCryptoFactory().Create(null, publicKeyXml, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

            var userLicense = Format(_userData, 1);
            var verified = verifier.VerifyHash(userLicense, signed);

            Assert.True(verified);
        }

        [Fact]
        public void Sign_Verify_Correct_Data_Success_Md5()
        {
            var generator = new RsaCryptoFactory().Create(HashAlgorithmName.MD5, RSASignaturePadding.Pkcs1);

            var licenseText = Format(_userData, 1);

            var signed = generator.SignHash(licenseText);

            var publicKeyXml = generator.PublicKey;

            Assert.NotNull(signed);

            var verifier = new RsaCryptoFactory().Create(null, publicKeyXml, HashAlgorithmName.MD5, RSASignaturePadding.Pkcs1);

            var userLicense = Format(_userData, 1);
            var verified = verifier.VerifyHash(userLicense, signed);

            Assert.True(verified);
        }

        [Fact]
        public void Sign_Verify_Correct_Data_Success_Sha384()
        {
            var generator = new RsaCryptoFactory().Create(HashAlgorithmName.SHA384, RSASignaturePadding.Pkcs1);

            var licenseText = Format(_userData, 1);

            var signed = generator.SignHash(licenseText);

            var publicKeyXml = generator.PublicKey;

            Assert.NotNull(signed);

            var verifier = new RsaCryptoFactory().Create(null, publicKeyXml, HashAlgorithmName.SHA384, RSASignaturePadding.Pkcs1);

            var userLicense = Format(_userData, 1);
            var verified = verifier.VerifyHash(userLicense, signed);

            Assert.True(verified);
        }

        [Fact]
        public void Sign_Verify_Correct_Data_Success_Sha1()
        {
            var generator = new RsaCryptoFactory().Create(HashAlgorithmName.SHA1, RSASignaturePadding.Pkcs1);

            var licenseText = Format(_userData, 1);

            var signed = generator.SignHash(licenseText);

            var publicKeyXml = generator.PublicKey;

            Assert.NotNull(signed);

            var verifier = new RsaCryptoFactory().Create(null, publicKeyXml, HashAlgorithmName.SHA1, RSASignaturePadding.Pkcs1);

            var userLicense = Format(_userData, 1);
            var verified = verifier.VerifyHash(userLicense, signed);

            Assert.True(verified);
        }

        [Fact]
        public void Sign_Verify_Correct_Data_Large_String_Success()
        {
            var generator = GetGenerator();

            var longUserData = $"{_userData}{_userData}{_userData}{_userData}{_userData}{_userData}{_userData}{_userData}";

            var licenseText = Format(longUserData, 1);

            var signed = generator.SignData(licenseText);

            Assert.NotNull(signed);

            var verifier = GetVerifier();

            var userLicense = Format(longUserData, 1);
            var verified = verifier.VerifyData(userLicense, signed);

            Assert.True(verified);
        }

        [Fact]
        public void SignData_Verify_Correct_Data()
        {
            var generator = new RsaCryptoFactory().Create();

            var longUserData = $"{_userData}";

            var licenseText = Format(longUserData, 1);

            var signed = generator.SignData(licenseText);

            Assert.NotNull(signed);
            Assert.NotNull(generator.PrivateKey);
            Assert.NotNull(generator.PublicKey);
            
            var verifier = new RsaCryptoFactory().Create(null, generator.PublicKey);

            var userLicense = Format(longUserData, 1);
            var verified = verifier.VerifyData(userLicense, signed);

            Assert.True(verified);
        }

        [Fact]
        public void Hash_Sign_Verify_Correct_Data_Success()
        {
            var generator = GetGenerator();

            var licenseText = Format(_userData, 1);

            var signed = generator.SignHash(licenseText);

            Assert.NotNull(signed);

            var verifier = GetVerifier();

            var userLicense = Format(_userData, 1);
            var verified = verifier.VerifyHash(userLicense, signed);

            Assert.True(verified);
        }

        [Fact]
        public void Hash_Sign_Verify_No_Keys_Correct_Data_Success()
        {
            var generator = new RsaCryptoFactory().Create();

            var licenseText = Format(_userData, 1);

            var signed = generator.SignHash(licenseText);

            var publicKey = generator.PublicKey;

            Assert.NotNull(signed);

            var verifier = new RsaCryptoFactory().Create(null, publicKey);

            var userLicense = Format(_userData, 1);
            var verified = verifier.VerifyHash(userLicense, signed);

            Assert.True(verified);
        }

        [Fact]
        public void Sign_Verify_Incorrect_Data_Fails()
        {
            var generator = GetGenerator();

            var licenseText = Format(_userData, 1);

            var signed = generator.SignData(licenseText);

            Assert.NotNull(signed);

            var verifier = GetVerifier();

            var userLicense = Format(_userDataAlt, 1);
            var verified = verifier.VerifyData(userLicense, signed);

            Assert.False(verified);
        }

        [Fact]
        public void Hash_Sign_Verify_Incorrect_Data_Fails()
        {
            var generator = GetGenerator();

            var licenseText = Format(_userData, 1);

            var signed = generator.SignHash(licenseText);

            Assert.NotNull(signed);

            var verifier = GetVerifier();

            var userLicense = Format(_userDataAlt, 1);
            var verified = verifier.VerifyHash(userLicense, signed);

            Assert.False(verified);
        }

        [Fact]
        public void Sign_Returns_Value()
        {
            var sut = GetGenerator();

            var licenseText = Format(_userData, 1);

            var signed = sut.SignData(licenseText);

            Assert.NotNull(signed);
        }

        [Fact]
        public void Throws_When_Verifying_Data_Not_Initialised()
        {
            var encoder = new UTF8Encoding();
            byte[] ba = encoder.GetBytes(_userData);
            var b64 = Convert.ToBase64String(ba);
            Assert.Throws<ArgumentException>(() => new RsaCryptoFactory().Create().VerifyData(_userData, b64));
        }

        [Fact]
        public void Throws_When_Verifying_Hash_Not_Initialised()
        {
            var encoder = new UTF8Encoding();
            byte[] ba = encoder.GetBytes(_userData);
            var b64 = Convert.ToBase64String(ba);
            Assert.Throws<ArgumentException>(() => new RsaCryptoFactory().Create().VerifyHash(_userData, b64));
        }

        [Fact]
        public void Throws_When_SignData_Message_Null()
        {
            var cryptoService = GetGenerator();
            Assert.Null(cryptoService.SignData(null));
        }

        [Fact]
        public void Throws_When_SignHash_Message_Null()
        {
            var cryptoService = GetGenerator();
            Assert.Null(cryptoService.SignHash(null));
        }

        [Fact]
        public void Throws_When_VerifyHash_SignedMessage_Null()
        {
            var cryptoService = GetVerifier();
            Assert.False(cryptoService.VerifyHash("s", null));
        }

        [Fact]
        public void Throws_When_VerifyHash_OriginalMessage_Null()
        {
            var cryptoService = GetVerifier();
            Assert.False(cryptoService.VerifyHash(null, "c"));
        }

        [Fact]
        public void Throws_When_Keys_Invalid()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                new RsaCryptoFactory().Create("c", "c");
            });
        }

        [Fact]
        public void Throws_When_VerifyData_SignedMessage_Null()
        {
            var cryptoService = GetVerifier();
            Assert.False(cryptoService.VerifyData("d", null));
        }

        [Fact]
        public void Throws_When_VerifyData_OriginalMessage_Null()
        {
            var cryptoService = GetVerifier();
            Assert.False(cryptoService.VerifyData(null, "c"));
        }

        [Fact]
        public void Throws_When_No_Xml_Keys_Initialised()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                new RsaCryptoFactory().Create(null, null);
            });
        }

        [Fact]
        public void Throws_When_No_RSAParameters_Keys_Initialised()
        {
            var rsaPrivate = new RSAParameters();
            var rsaPublic = new RSAParameters();

            Assert.Throws<ArgumentException>(() =>
            {
                new RsaCryptoFactory().Create(rsaPrivate, rsaPublic);
            });
        }

        [Fact]
        public void IsDisposed()
        {
            var sut = new RsaCryptoFactory().Create(_privateKey, _publicKey);
            using (sut)
            {
                Assert.NotNull(sut.PublicKey);
                Assert.NotNull(sut.PrivateKey);
            }

            Assert.Null(sut.PublicKey);
            Assert.Null(sut.PrivateKey);
        }

        private string Format(string s, int i)
        {
            return $"{s}{i}";
        }
    }
}
