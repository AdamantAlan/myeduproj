package main

import (
	"fmt"
	"hello/services"
)

type User = services.User

func main() {
	user := User{Name: "Dima", Age: 28, IsAdmin: false}
	SetAdmin(&user)

	fmt.Printf("User %v\n", user.Name)
	fmt.Printf("User age %v\n", user.Age)
	fmt.Printf("User is admin? %v\n", user.IsAdmin)
}

func SetAdmin(user *User) bool {
	return user.SetAdmin()
}
