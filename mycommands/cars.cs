using HtmlAgilityPack;
using qdevopsbase.server;
using System;
using System.IO;
using System.Xml;

public class cars : icustomizehtml
{
	public string CustomizeHtml(string objid, string html)
	{
		HtmlDocument doc = new HtmlDocument();
		doc.LoadHtml(html);

		if (objid == "1º objeto") doc.CreateNode("<h2>Resumo de vendas por Loja</h2>");
		if (objid == "2º objeto") doc.CreateNode("<h2>Resumo de vendas por Vendedor</h2>");
		if (objid == "3º objeto") doc.CreateNode("<h2>Resumo de vendas por Marca</h2>");

		//copiando o cabeçalho
		var cab_node = doc.DocumentNode.SelectSingleNode("//tr").CloneNode(true);

		foreach (HtmlNode td in doc.DocumentNode.SelectNodes("//td"))
		{
			if (td.InnerText.ToLower().Trim() == "total")
			{
				td.ParentNode.Attributes.Add("class", "n1");
				td.ParentNode.ParentNode.AppendChild(HtmlNode.CreateNode("<tr><td>&nbsp;</td></tr>"));
				td.ParentNode.AppendChild(cab_node);
			}
			if (td.InnerText.ToLower().Trim() == "total geral") td.ParentNode.Attributes.Add("class", "n2");
		}




		string ret;
		using (var writer = new StringWriter())
		{
			doc.Save(writer);
			ret = writer.ToString();
		}

		return ret;
	}
}
