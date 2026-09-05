package numeric

import (
	"fmt"
	"math"
)

func One() {
	fmt.Println("1.")
	defer end()

	var x = 10
	var y int64 = int64(x)

	fmt.Printf("%T %v\n", x, x)
	fmt.Printf("%T %v\n", y, y)
	fmt.Println(int64(x) == y)
}

func OneOne() {
	fmt.Println("1.1")
	defer end()

	const x = 10
	var y int64 = x

	fmt.Printf("%T %v\n", x, x)
	fmt.Printf("%T %v\n", y, y)
	fmt.Println(int64(x) == y)
}

func Three() {
	fmt.Println("3")
	defer end()

	var x = 10.9
	var y = int(x)

	fmt.Printf("%T %v\n", x, x)
	fmt.Printf("%T %v\n", y, y)
	fmt.Println(int(x) == y)
}

func Four() {
	fmt.Println("4")
	defer end()

	var x = -10
	var y = uint(x)

	fmt.Printf("%T %v\n", x, x)
	fmt.Printf("%T %v\n", y, y)
	fmt.Println(uint(x) == y)
}

func Seven() {
	fmt.Println("7")
	defer end()

	const X = 10

	var a int32 = X
	var b int64 = X
	var c float64 = X

	fmt.Println(a, b, c)

	fmt.Printf("%T %v\n", a, a)
	fmt.Printf("%T %v\n", b, b)
	fmt.Printf("%T %v\n", c, c)
}

func Sixteen() {
	fmt.Println("16")
	defer end()

	const A = 10
	const B = 3

	var x = A / B
	var y = float64(A / B)
	var z = math.Round(float64(A)/float64(B)*100) / 100

	fmt.Printf("%T %v\n", x, x)
	fmt.Printf("%T %v\n", y, y)
	fmt.Printf("%T %v\n", z, z)
}

func Seventeen() {
	fmt.Println("17")
	defer end()

	const A = 10.0
	const B = 3

	var x = A / B
	var y = float64(A / B)
	var z = math.Round(float64(A)/float64(B)*100) / 100

	fmt.Printf("%T %v\n", x, x)
	fmt.Printf("%T %v\n", y, y)
	fmt.Printf("%T %v\n", z, z)
}

func Eightteen() {
	fmt.Println("18")
	defer end()

	const A = 10
	const B float64 = 3

	var x = A / B
	var y = float64(A / B)
	var z = math.Round(float64(A)/float64(B)*100) / 100

	fmt.Printf("%T %v\n", x, x)
	fmt.Printf("%T %v\n", y, y)
	fmt.Printf("%T %v\n", z, z)
}

func Twenty() {
	fmt.Println("20")
	defer end()

	const X = 255

	var a = X
	var b byte = byte(a)

	fmt.Printf("%T %v\n", a, a)
	fmt.Printf("%T %v\n", b, b)
}

func end() {
	fmt.Println("---")
}
