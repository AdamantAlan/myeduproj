"use strict"
function GetUser() {
	alert("HELLO");
let userForm = document.forms.user;
function userConstructor(name,age)
{
		
				this.name:name;
				this.age:age
			
}

let getUser = new userConstructor(userForm.name,userForm.age);
alert(getUser.name);
alert(userForm.name);
}

$('#sub').bind('click', GetUser);