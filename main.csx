using BCP.Neplan.Helpers.Script;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.Runtime.InteropServices;



ShowMessageWindow();
ClearMessages();
SendInfoMessage("***************************************************************************************** ");
SendInfoMessage("************* Start of the script to influence coordination process aplication************ ");
SendInfoMessage("***************************************************************************************** ");

Dictionary<string, string> allElementsOfVariante = new Dictionary<string, string>();
GetAllElementsOfVariant(out allElementsOfVariante);
SendInfoMessage("-- > All elements of the variant were obtained");


// SendInfoMessage(allElementsOfVariante);

foreach( var element in allElementsOfVariante)
{
    // // SendInfoMessage("-- > Element name:");
    // SendInfoMessage(element.Key);
    // SendInfoMessage("-- > Elements type:");
    // SendInfoMessage(element.Value);
    if (element.Key == "L3-2")
    {
        SendInfoMessage("encontré la linea L3-2");

        bool ejectutarAnalisis = RunCurrentAnalysis();
        SendInfoMessage("Run current analysis...");
        SendInfoMessage("update diagram");
        UpdateTopologyAndDiagram();




    }



}

