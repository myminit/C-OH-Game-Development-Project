package com.coh.cohBackend.controller;

import com.coh.cohBackend.services.CorrectService;
import jakarta.servlet.http.HttpSession;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.PutMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("/api/quests")
public class CorrectController {

    private final CorrectService correctService;

    @Autowired
    public CorrectController(CorrectService correctService) {
        this.correctService = correctService;
    }

    @PutMapping("/mark-correct")
    public ResponseEntity<String> markQuestAsCorrect(@RequestParam int questId, Double time, HttpSession session) {
        Integer userId = (Integer) session.getAttribute("userId");

        if (userId == null) {
            return new ResponseEntity<>("User not logged in", HttpStatus.UNAUTHORIZED);
        }

        try {
            boolean isUpdated = correctService.markQuestAsCorrect(userId, questId, time);

            if (isUpdated) {
                return new ResponseEntity<>("Quest marked as correct!", HttpStatus.OK);
            } else {
                return new ResponseEntity<>("Failed to mark quest as correct.", HttpStatus.INTERNAL_SERVER_ERROR);
            }
        } catch (Exception e) {
            e.printStackTrace();
            return new ResponseEntity<>("Internal Server Error: " + e.getMessage(), HttpStatus.INTERNAL_SERVER_ERROR);
        }
    }

}
