"use strict";    
const z = 2;
function sq (area, ...sides)
{
    if(sides.length == 2)
        return [area, sides[0]*sides[1]*z];

    let g = 1;
    return [area, -1];
}

var res = sq("Quadro", 2,2);
document.write(`${res[0]}=${res[1]}`);

function outer(){
    let x = 5;
    function inner(){
        x++;
        console.log(x);
    };
    return inner;
}
const fn = outer(); // fn = inner, так как функция outer возвращает функцию inner
// вызываем внутреннюю функцию inner
fn();   // 6
fn();   // 7
fn();   // 8


function print(){
    console.log("Доброе утро");
    print = function(){
        console.log("Добрый день");
    }
}
print(); // Доброе утро
print(); // Добрый день
const printMessage = print;
printMessage(); // Добрый день
printMessage(); // Добрый день

console.log(foo);   // undefined
var foo = "Tom";

display();
 
var display = function (){
    console.log("Hello Hoisting");
}