package com.coh.cohBackend.repository;

import lombok.Data;

@Data
public class LoginResponse {

    private String status;
    private String userID;
    private String message;

    public LoginResponse(String status, String userID, String message) {
        this.status = status;
        this.userID = userID;
        this.message = message;
    }

    public String getStatus() {
        return status;
    }

    public void setStatus(String status) {
        this.status = status;
    }

    public String getUserID() {
        return userID;
    }

    public void setUserID(String userID) {
        this.userID = userID;
    }

    public String getMessage() {
        return message;
    }

    public void setMessage(String message) {
        this.message = message;
    }
}
