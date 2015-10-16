using System;
using System.IO;
using System.Text;
/// <summary>
/// Byte array.重写的网络工具类，为了使用现有通讯框架的，负责提供解析二进制数据的接口
/// </summary>
/// <exception cref='Exception'>
/// Is thrown when the exception.
/// </exception>
/// <exception cref='FormatException'>
/// Is thrown when the format exception.
/// </exception>
public class ByteArray
{
    private MemoryStream buffer;
    BinaryReader br;
    BinaryWriter bw;
    private bool compressed = false;
    public byte[] Bytes
    {
        get
        {
            return this.buffer.ToArray();
        }
    }
    public int Length
    {
        get
        {
            return (int)buffer.Length;
        }
        set
        {
            buffer.SetLength(value);
            buffer.Position = 0;
        }
    }
    public int Position
    {
        get
        {
            return (int)buffer.Position;
        }
        set
        {
            buffer.Position = value;
        }
    }
    public int BytesAvailable
    {
        get
        {
            int num = this.buffer.GetBuffer().Length - Position;
            if (num > this.buffer.GetBuffer().Length || num < 0)
            {
                num = 0;
            }
            return num;
        }
    }
    public bool Compressed
    {
        get
        {
            return this.compressed;
        }
        set
        {
            this.compressed = value;
        }
    }
    public ByteArray()
    {
        this.buffer = new MemoryStream();
        br = new BinaryReader(buffer);
        bw = new BinaryWriter(buffer);
    }
    public ByteArray(byte[] buf)
    {
        this.buffer = new MemoryStream();
        this.buffer.Write(buf, 0, buf.Length);
        this.buffer.Position = 0;
        br = new BinaryReader(buffer);
        bw = new BinaryWriter(buffer);
    }

    public ByteArray(MemoryStream stream, BinaryReader reader)
    {
        buffer = stream;
        br = reader;
    }

    public void Compress()
    {
        throw new Exception("Compress not implemented!!");
        //			if (this.compressed)
        //			{
        //				throw new Exception("Buffer is already compressed");
        //			}
        //			MemoryStream memoryStream = new MemoryStream();
        //			using (ZOutputStream zOutputStream = new ZOutputStream(memoryStream, 9))
        //			{
        //				zOutputStream.Write(this.buffer, 0, this.buffer.Length);
        //				zOutputStream.Flush();
        //			}
        //			this.buffer = memoryStream.ToArray();
        //			this.position = 0;
        //			this.compressed = true;
    }
    public void Uncompress()
    {
        throw new Exception("Uncompress not implemented!!");
        //			MemoryStream memoryStream = new MemoryStream();
        //			using (ZOutputStream zOutputStream = new ZOutputStream(memoryStream))
        //			{
        //				zOutputStream.Write(this.buffer, 0, this.buffer.Length);
        //				zOutputStream.Flush();
        //			}
        //			this.buffer = memoryStream.ToArray();
        //			this.position = 0;
        //			this.compressed = false;
    }

    private void CheckCompressedWrite()
    {
        if (this.compressed)
        {
            throw new Exception("Only raw bytes can be written a compressed array. Call Uncompress first.");
        }
    }

    private void CheckCompressedRead()
    {
        if (this.compressed)
        {
            throw new Exception("Only raw bytes can be read from a compressed array.");
        }
    }

    public void WriteByte(byte b)
    {
        bw.Write(b);
    }
    public void WriteBytes(byte[] data)
    {
        this.WriteBytes(data, 0, data.Length);
    }
    public void WriteBytes(byte[] data, int ofs, int count)
    {
        this.buffer.Write(data, ofs, count);
    }
    public void WriteBool(bool b)
    {
        this.CheckCompressedWrite();
        bw.Write(b);
    }
    public void WriteInt(int i)
    {
        this.CheckCompressedWrite();
        bw.Write(i);
    }

    public void WriteUInt(uint ui)
    {
        this.CheckCompressedWrite();
        bw.Write(ui);
    }

    public void WriteUShort(ushort us)
    {
        this.CheckCompressedWrite();
        bw.Write(us);
    }
    public void WriteShort(short s)
    {
        this.CheckCompressedWrite();
        bw.Write(s);
    }
    public void WriteLong(long l)
    {
        this.CheckCompressedWrite();
        bw.Write(l);
    }
    public void WriteFloat(float f)
    {
        this.CheckCompressedWrite();
        bw.Write(f);
    }
    public void WriteDouble(double d)
    {
        this.CheckCompressedWrite();
        bw.Write(d);
    }
    public void WriteUTF(string str)
    {
        this.CheckCompressedWrite();
        byte[] buffer = Encoding.UTF8.GetBytes(str);
        if (buffer.Length > short.MaxValue)
        {
            throw new FormatException("String length cannot be greater then 32768 !");
        }
        bw.Write((short)buffer.Length);
        bw.Write(buffer);
    }
    public byte ReadByte()
    {
        this.CheckCompressedRead();
        return br.ReadByte();
    }
    public byte[] ReadBytes(int count)
    {
        return br.ReadBytes(count);
    }
    public bool ReadBool()
    {
        this.CheckCompressedRead();
        return br.ReadBoolean();
    }
    public int ReadInt()
    {
        this.CheckCompressedRead();
        return br.ReadInt32();
    }

    public uint ReadUInt()
    {
        this.CheckCompressedRead();
        return br.ReadUInt32();
    }

    public ushort ReadUShort()
    {
        this.CheckCompressedRead();
        return br.ReadUInt16();
    }
    public short ReadShort()
    {
        this.CheckCompressedRead();
        return br.ReadInt16();
    }
    public long ReadLong()
    {
        this.CheckCompressedRead();
        return br.ReadInt64();
    }
    public float ReadFloat()
    {
        this.CheckCompressedRead();
        return br.ReadSingle();
    }
    public double ReadDouble()
    {
        this.CheckCompressedRead();
        return br.ReadDouble();
    }
    public string ReadUTF()
    {
        this.CheckCompressedRead();
        ushort size = br.ReadUInt16();
        return Encoding.UTF8.GetString(br.ReadBytes(size));
    }
}


