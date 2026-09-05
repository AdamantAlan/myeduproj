package str

import (
	"fmt"
	"strconv"
	//"math"
)

func One() {
	fmt.Println("1-6")
	defer end()

	var r1 rune = 'Ё'
	fmt.Println(r1)
	fmt.Printf("%c\n", r1)

	var r2 rune = 'Я'
	fmt.Println(r2)
	fmt.Println(string(r2))

	var x int = 65
	fmt.Println(string(x))

	var r3 rune = 'A'
	var s1 string = string(r3)
	fmt.Println(s1)
}

func Seven() {
	fmt.Println("7")
	defer end()

	s := "Hello"

	fmt.Println(len(s))
	fmt.Println(s[0])
	fmt.Println(string(s[0]))
}

func Ten() {
	fmt.Println("10")
	defer end()

	s := "Привет"

	r := []rune(s)

	fmt.Println(len(r))
	fmt.Println(r[0])
	fmt.Println(string(r[0]))
}

func Eleven() {
	fmt.Println("11")
	defer end()

	s := "AЯ"

	for _, r := range s {
		fmt.Println(string(r))
	}
}

func Thirteen() {
	fmt.Println("14")
	defer end()

	s := "Hello"
	r := []rune(s)
	r[0] = 'h'
	fmt.Println(string(r))

	s1 := "Приветя"
	b := []byte(s1)
	r1 := []rune(s1)

	fmt.Println(len(s1))
	fmt.Println(len(b))
	fmt.Println(len(r1))
	fmt.Println(string(r1))
}

func Sixteen() {
	fmt.Println("16")
	defer end()

	r := []rune{'П', 'р', 'и', 'в', 'е', 'т', 'я'}

	s := string(r)

	fmt.Println(s)
	fmt.Println(len(s))
	fmt.Println(len(r))

	b := []byte{72, 101, 108, 108, 111}

	fmt.Println(string(b))
}

func Eightteen() {
	fmt.Println("18")
	defer end()

	s := "Я"

	b := []byte(s)

	fmt.Println(b)
	fmt.Println(s)
	fmt.Println(len(b))
	fmt.Println(len(s))

	s1 := "Я"

	r1 := []rune(s1)
	b1 := []byte(s1)

	fmt.Println(r1)
	fmt.Println(b1)
	fmt.Println(string(b1))
}

func Twenty() {
	fmt.Println("20")
	defer end()

	s := "A😊Б"
	fmt.Println(len(s))
	fmt.Println(len([]rune(s)))

	for i, r := range s {
		fmt.Printf("%d %c\n", i, r)
	}
}

func Concat() {
	fmt.Println("Concat1")
	defer end()
	a1 := "Hello"
	b1 := "World"

	fmt.Println(a1 + " " + b1)
	fmt.Println("Concat2")
	name2 := "Bob"
	age2 := 25

	s2 := "Name: " + name2 + ", age: " + strconv.Itoa(age2)

	fmt.Println(s2)

	fmt.Println("Concat5")
	name5 := "Bob"
	age5 := 25
	height5 := 1.83

	s5 := fmt.Sprintf("%s is %d years old and %.1f m tall", name5, age5, height5)

	fmt.Println(s5)

	fmt.Println("Concat7")
	r7 := 'A'

	s7 := "Letter: " + string(r7)

	fmt.Println(s7)

	fmt.Println("Concat8")
	b8 := byte(65)

	s8 := "Letter: " + string(b8)

	fmt.Println(s8)

	fmt.Println("Concat9")
	x9 := 65

	fmt.Println("Value: " + string(x9))
	fmt.Println("Value: ", x9)

	fmt.Println("Concat10")
	name11 := "Bob"
	var age11 byte = 25

	fmt.Println("Name:", name11, "Age:", age11)
	fmt.Printf("Name: %s Age: %d\n", name11, age11)
	fmt.Println(fmt.Sprintf("Name: %s Age: %d", name11, age11))

	r11 := []rune(name11)

	if r11[0] == 'B' {
		r11[0] = 'b'
	}

	name12 := "Bob"
	if name12 == name11 {
		fmt.Println(strconv.FormatBool(true))
	}
	name11 = string(r11)
	fmt.Println("Name:", name11, "Age:", age11)
}

func end() {
	fmt.Println("---")
}
