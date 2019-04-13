using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
public class GlyphPoint
{
    public Vector2 vector;
    public double angle;
    public GlyphPoint()
    {
        vector = new Vector2(0, 0);
        angle = 0.0f;
    }
    public GlyphPoint(Vector3 _vec, double _angle)
    {
        vector = _vec;
        angle = _angle;
    }
    public double getDifference(GlyphPoint cp)
    {//разница идет в пикселях + углах
        double difference = 0;
        difference += Math.Abs(vector.magnitude - cp.vector.magnitude) < 3 ? 0 : Math.Abs(vector.magnitude - cp.vector.magnitude);
        //double angleDif = Math.Acos((vector.x * cp.vector.x + vector.y * cp.vector.y) / (vector.magnitude * cp.vector.magnitude)) * 180 / Math.PI;
        //difference += Math.Abs(angleDif) < 10 ? 0 : Math.Abs(angleDif);
        difference += Math.Abs(angle - cp.angle) < 10 ? 0 : Math.Abs(angle - cp.angle);
        return difference;
    }
    override
    public string ToString()
    {
        return vector.ToString() + " " + angle;
    }
    public void ExportToFile(BinaryWriter writer)
    {
        writer.Write(vector.x);
        writer.Write(vector.y);
        writer.Write(angle);
    }
    public GlyphPoint ImportFromFile(BinaryReader reader)
    {
        vector.x = reader.ReadSingle();
        vector.y = reader.ReadSingle();
        angle = reader.ReadDouble();
        return this;
    }
}

public class Glyph
{
    public string glyphName;
    List<GlyphPoint> points;
    public string filepath = ".\\Assets\\Scripts\\Glyphs\\";
    public string extension = ".glyph";
    public Glyph()
    {
        glyphName = "";
        points = new List<GlyphPoint>();
    }
    public Glyph(string _name)
    {
        glyphName = _name;
        points = new List<GlyphPoint>();
    }
    public Glyph(string _name, List<GlyphPoint> _points)
    {
        glyphName = _name;
        points = _points;
    }
    public double CompareGlyph(Glyph _glyph)
    {
        double difference = 0;
        for (int i = 0; i < points.Count; i++)
        {
            difference += points[i].getDifference(_glyph.points[i]);
        }
        return difference;
    }
    public bool ExportGlyph(string filename = "")
    {
        if (filename.Length == 0) filename = filepath + glyphName + extension;
        try
        {
            BinaryWriter writer = new BinaryWriter(File.Open(filename, FileMode.Create));
            foreach (GlyphPoint gp in points)
                gp.ExportToFile(writer);
            writer.Close();
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
            return false;
        }
        return true;
    }
    public bool ImportGlyph(string filename = "")
    {
        if (filename.Length == 0) filename = filepath + glyphName + extension;
        try
        {
            BinaryReader reader = new BinaryReader(File.Open(filename, FileMode.Open));
            points.Clear();
            while (reader.PeekChar() > -1)
                points.Add((new GlyphPoint()).ImportFromFile(reader));
            reader.Close();
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
            return false;
        }
        return true;
    }
}

public class GlyphsDictionary
{
    List<Glyph> glyphDict;
    // Start is called before the first frame update
    public GlyphsDictionary()
    {
        glyphDict = new List<Glyph>();
        ImportGlyphs();
    }

    public void ImportGlyphs()
    {
        string[] file_list = Directory.GetFiles(new Glyph().filepath, "*.glyph");
        glyphDict.Clear();
        foreach(string fname in file_list)
        {
            Glyph gl = new Glyph(Path.GetFileNameWithoutExtension(fname));
            if (gl.ImportGlyph())
                glyphDict.Add(gl);
        }
        
    }
    
    public Tuple<double,  Glyph> FindGlyph(Glyph glyph)
    {
        int index = -1;
        double minDiffernce = GlyphRecognition.maxGlyphDifference;
        for(int i = 0; i<glyphDict.Count; i++)
        {
            double tmpDifference = glyphDict[i].CompareGlyph(glyph);
            if (minDiffernce > tmpDifference)
            {
                minDiffernce = tmpDifference;
                index = i;
            }
        }
        if (index != -1)
            return new Tuple<double, Glyph>(minDiffernce, glyphDict[index]);
        else
            return new Tuple<double, Glyph>(0, new Glyph());
    }
}
