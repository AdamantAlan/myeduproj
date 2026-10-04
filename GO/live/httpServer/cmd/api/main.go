// ┌────────────────────┐
// │      cmd/api        │
// │ composition root   │
// └──────────┬─────────┘
//            │
//            ▼
// ┌────────────────────┐
// │     delivery       │
// │ Gin handlers       │
// └──────────┬─────────┘
//            │
//            ▼
// ┌────────────────────┐
// │    application     │
// │ use cases/service  │
// │ repository iface   │
// └──────────┬─────────┘
//            │
//            ▼
// ┌────────────────────┐
// │       domain       │
// │ entities/rules     │
// └────────────────────┘

// ┌────────────────────┐
// │  infrastructure    │
// │ Memory/Postgres    │
// └──────────┬─────────┘
//            │ implements
//            ▼
//      Repository

package main

import (
	"clean-gin/internal/server"
	"log"

	userapp "clean-gin/internal/user/application"
	userhttp "clean-gin/internal/user/delivery/http"
	userinfra "clean-gin/internal/user/infrastructure"
)

func main() {
	// User dependencies.
	userRepository := userinfra.NewMemoryRepository()
	userService := userapp.NewService(userRepository)
	userHandler := userhttp.NewHandler(userService)

	// HTTP server.
	router := server.NewRouter(
		userHandler,
	)

	if err := router.Run(":8080"); err != nil {
		log.Fatal(err)
	}
}
