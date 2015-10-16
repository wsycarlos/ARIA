using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Template.Editor;

public class LocalizationDataWriter : ITemplaterWriter
{
    public ByteArray GenerateByteArray(object data)
    {
        List<LanguageVO> list = data as List<LanguageVO>;
        ByteArray ba = new ByteArray();
        int length = list.Count;
        ba.WriteInt(length);
        for (int i = 0; (i < length); i = (i + 1))
        {
            ba.WriteUTF(list[i].key);
            ba.WriteUTF(list[i].Value);
        }
        return ba;
    }
}