using UnityEngine;
//using UnityEngine.UI;
using UnityEngine.UIElements;
using System.Collections;

public class RobotSourceCodeEditor : MonoBehaviour {
	
	private UIDocument _doc;
    private Button _executeButton;

	private TextField _textField;
	public RuntimeScriptable RuntimeScriptableObject;

	private void Awake(){
		_doc = GetComponent<UIDocument>();
        _textField = _doc.rootVisualElement.Q<TextField>("TextField");
        _executeButton = _doc.rootVisualElement.Q<Button>("ExecuteButton");
        _executeButton.clicked += OnCompileAndRunClick;

	}

	public void OnCompileAndRunClick()
	{
		if(RuntimeScriptableObject != null)
		{
			RuntimeScriptableObject.CompileAndRun(_textField.text);
		}
	}
}
