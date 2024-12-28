package com.coh.cohBackend.config;

import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.jdbc.datasource.DriverManagerDataSource;

@Configuration
public class DatabaseConfig {

    @Bean
    public JdbcTemplate jdbcTemplate() {
        DriverManagerDataSource dataSource = new DriverManagerDataSource();
        dataSource.setUrl("jdbc:mysql://localhost:3306/cohcode");
        dataSource.setUsername("root");
        dataSource.setPassword("66090500413");
        dataSource.setDriverClassName("com.mysql.cj.jdbc.Driver");

        return new JdbcTemplate(dataSource);
    }
}
