//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
namespace AssemblyCSharp
{
	interface ISave
	{
		void doSave(object data,string savedName);
		object doLoad(string savedName);
		//for check file only, if there is no data in path return false
		bool isHaveData(string savedName);
	}
}
