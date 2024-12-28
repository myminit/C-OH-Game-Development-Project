# Build stage: ใช้ Maven + JDK 23 สำหรับคอมไพล์
FROM maven:3-eclipse-temurin-23 AS build
WORKDIR /app

COPY . .
RUN mvn clean package -DskipTests

# Runtime stage: ใช้ JDK 23 สำหรับรันแอป
FROM eclipse-temurin:23-jre-alpine
WORKDIR /app

COPY --from=build /app/target/*.jar app.jar

EXPOSE 8080

ENTRYPOINT ["java", "-jar", "app.jar"]
