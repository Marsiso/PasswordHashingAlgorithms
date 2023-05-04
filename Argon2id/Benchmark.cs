using System.Security.Cryptography;
using System.Text;
using BenchmarkDotNet.Attributes;
using SodiumLibrary.Enums;
using static SodiumLibrary.Argon2;

namespace Argon2id;

[MemoryDiagnoser]
public class Benchmark
{
    private const int KeyLength = 32;
    private const int PasswordLength = 12;
    private const int SaltLength = 16;
    private const char Delimiter = ';';
    private const int MemoryHardness = 268_435_456;
    private const long Cycles = 16;
    private const ArgonType AlgorithmType = ArgonType.Argon2Id;
    private static readonly byte[] PasswordAsBytes = RandomNumberGenerator.GetBytes(PasswordLength);
    private static readonly byte[] SaltAsBytes = RandomNumberGenerator.GetBytes(SaltLength);

    [Benchmark]
    public string Hash()
    {
        var keyBuffer = new byte[KeyLength];
        crypto_pwhash(
            keyBuffer, 
            KeyLength, 
            PasswordAsBytes, 
            PasswordAsBytes.LongLength, 
            SaltAsBytes, 
            Cycles,
            MemoryHardness, 
            (int)AlgorithmType);
        
        return string.Format("{0}{1}{2}",
            Convert.ToBase64String(SaltAsBytes),
            Delimiter,
            Convert.ToBase64String(keyBuffer));
    }
}