using System.Runtime.InteropServices;

namespace SodiumLibrary;

/// <summary>
/// External library for efficient, cryptographically strong and secure password hash computation based on Argon2 key derivation algorithm.
/// </summary>
public static class Argon2
{
    /// <summary>
    /// The name of dynamically linked library that contains the unmanaged method.
    /// </summary>
    private const string DllName = "libsodium";

    /// <summary>
    /// Initializes the static class members.
    /// </summary>
    static Argon2() => sodium_init();

    /// <summary>
    /// Initializes the library, should be called before accessing library methods.
    /// </summary>
    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void sodium_init();

    /// <summary>
    /// Fills the buffer with unpredictable and cryptographically secure sequence of bytes.
    /// </summary>
    /// <param name="buffer">The buffer that stores a cryptographically secure and unpredictable sequence of bytes.</param>
    /// <param name="bufferSize">The buffer size in bytes.</param>
    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void randombytes_buf(byte[] buffer, int bufferSize);

    /// <summary>
    /// The key derivation function that computes key/password hash from the given password supplemented by the salt.
    /// </summary>
    /// <param name="buffer">The buffer that stores the derivation function output, key, as sequence of bytes.</param>
    /// <param name="bufferSize">The buffer size in bytes.</param>
    /// <param name="passwordBytes">The password as a sequence of bytes varying in length to be hashed.</param>
    /// <param name="passwordBytesSize">Length of the password byte sequence.</param>
    /// <param name="saltBytes">Sequence of bytes that are appended or prepended to the password byte sequence before it is hashed.</param>
    /// <param name="cycles">Cycles represents the maximum amount of computations to perform. Raising this number will make the function require more CPU cycles to compute a key. </param>
    /// <param name="memoryHardness">Memory is the maximum amount of RAM in bytes that the function will use.</param>
    /// <param name="algorithm">Alg is an identifier for the algorithm to use and should be set to one of the following values:
    /// 0 (default) - The currently recommended algorithm, which can change from one version of libsodium to another.
    /// 1 (Argon2i13) - Version 1.3 of the Argon2i algorithm.
    /// 2 (Argon2id13) - Version 1.3 of the Argon2id algorithm.
    /// </param>
    /// <returns></returns>
    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int crypto_pwhash(
        byte[] buffer,
        long bufferSize,
        byte[] passwordBytes,
        long passwordBytesSize,
        byte[] saltBytes,
        long cycles,
        int memoryHardness,
        int algorithm);
}