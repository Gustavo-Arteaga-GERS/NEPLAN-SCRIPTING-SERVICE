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


// Definimos la carpeta del proyecto
//Aquí se debe definir su carpeta del proyecto
string folder = "D:\\PROJECTS\\INFLUENCE-COORDINATION-PROCESS";


//Guardar los datos en una variable
//datos del elemento de falla
string inputElementosDeFalla = @""+ folder + "\\ElementosDeFalla.csv";
//Datos del elemento en donde queremos leer los resultados
string elementosDeAnalisis = @""+ folder + "\\ElemenetosDeAnalisis.csv";

//Guardar los elementos leidos cómo una lista
string[] elementosDeFalla = File.ReadAllLines(inputElementosDeFalla);
string[] elementosAnalisis = File.ReadAllLines(elementosDeAnalisis);




//Configurar los parametros del análisis
string analysisType = "ShortCircuit";
string attributeName = "FaultType";
// 0 = trifasica, 1 = monofasica,...
string attributeValue = "0";
SetCalcParameterAttribute(analysisType, attributeName,attributeValue);

//Porcentaje de ubicación de falla 
double distancia = 50.0D;

//lista de string para guardar los resultados
List<string> resultadosDeElementos = new List<string>();

//Leemos cada fila del archivo CSV
foreach( var nombreElementosFalla in elementosDeFalla)
{
    SendInfoMessage(nombreElementosFalla);
    
    
    //Definimos la línea y el porcentaje de falla
    bool falla = SetShortCircuitOnLine(nombreElementosFalla, distancia);

    if(falla == true)
    {
        //Ejecutar Análisis
        //Recordar quitar los elementos de falla desde la ventana de NEPLAN
        bool ejectutarAnalisis = RunCurrentAnalysis();   
        SendInfoMessage("Run current analysis...");
        SendInfoMessage("update diagram");
        // UpdateTopologyAndDiagram();

        //metodo para leer los elementos de análisis
        foreach (var elementoResultados in elementosAnalisis)
        {
            var itemElementoResultados = elementoResultados.Split(',');
            SendInfoMessage("********************************");
            SendInfoMessage(itemElementoResultados[0]);
            SendInfoMessage(itemElementoResultados[1]);
            SendInfoMessage("********************************");
            //función para obtener el resultado
            resultadosDeElementos = GetElementResultAtPort(itemElementoResultados[0],0);
   
        }




        RemoveShortCircuit(nombreElementosFalla);
        
    }

}

//metodo para leer los resultados de cada elemento
foreach ( var resultados in resultadosDeElementos)
{
    SendInfoMessage(resultados);
}

SendInfoMessage("end script 😎");