package main

import (
	"fmt"
	"hello/services"
	"math"
)

type User = services.User

func main() {
	var i *int = new(int(1))
	fmt.Println(*i)

	var user *User = new(User{Name: "Vasy"})
	fmt.Println(user.IsAdmin)

	*i = 30
	fmt.Println(*i)
	user.IsAdmin = true

	fmt.Println(*i)
	fmt.Println(user.IsAdmin)

	arr := [5]int{1, 2, 3, 4, 5}

	for i := 0; i < len(arr); i++ {
		fmt.Println(arr[i])
	}

	for i, r := range arr {
		fmt.Println(i, ",", r)
	}

	s := arr[0:3]

	for i1, r1 := range s {
		fmt.Println(i1, ",", r1)
	}
}

func PrintNumbers() {
	var sum int
	SumNum(&sum)
	fmt.Println("sum=", sum)

	var sum2 int = GetSum2(&sum)
	sum2 = MegaChangeSum2(sum, sum2)
	fmt.Println("sum2=", sum2)
}

func SwitchTrue(i int) {
	switch {
	case i < 10:
		fmt.Printf("%T\n", i)
	case 10 <= i && i < 100:
		fmt.Printf("%v\n", i)
	default:
	}
}

func SwitchFmt(i int) {
	switch v := math.Pow(float64(i), 2); v {
	case 1:
		fmt.Printf("%T\n", i)
	case 4:
		fmt.Printf("%v\n", i)
	default:
	}
}

func MegaChangeSum2(sum, sum2 int) int {
	for {
		sum2 += sum
		fmt.Println(sum2)

		if sum2 > 2000 {
			break
		} else if sum2 > 1950 {
			sum2--
		} else {
			sum2 -= 10
		}
	}

	return sum2
}

func GetSum2(sum *int) (sum2 int) {
	for sum2 < 1000 {
		sum2 += *sum
	}

	return
}

func SumNum(sum *int) {
	for i := 0; i < 10; i++ {
		*sum += i
	}
}

func SetAdmitToDima() {
	user := User{Name: "Dima", IsAdmin: false}
	SetAdmin(&user)

	fmt.Printf("User %v\n", user.Name)
	fmt.Printf("User age %v\n", user.Age)
	fmt.Printf("User is admin? %v\n", user.IsAdmin)
}

func SetAdmin(user *User) bool {
	return user.SetAdmin()
}
