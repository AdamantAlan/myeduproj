package server

import (
	userhttp "clean-gin/internal/user/delivery/http"

	"github.com/gin-gonic/gin"
)

func NewRouter(
	userHandler *userhttp.Handler,
) *gin.Engine {
	router := gin.Default()

	api := router.Group("/api")

	userHandler.RegisterRoutes(api)

	return router
}
