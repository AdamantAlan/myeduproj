package main

import (
	"fmt"
	"task/numeric"
	"task/str"
)

func main() {
	fmt.Println("numeric")
	numeric.One()
	numeric.OneOne()
	numeric.Three()
	numeric.Four()
	numeric.Seven()
	numeric.Sixteen()
	numeric.Seventeen()
	numeric.Eightteen()
	numeric.Twenty()

	fmt.Println("string")
	str.One()
	str.Seven()
	str.Ten()
	str.Eleven()
	str.Thirteen()
	str.Sixteen()
	str.Eightteen()
	str.Twenty()
	str.Concat()
}
