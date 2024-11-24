package com.coh.cohBackend.controller;

import com.coh.cohBackend.entity.Levels;
import com.coh.cohBackend.entity.Play;
import com.coh.cohBackend.services.CompleteService;
import com.coh.cohBackend.services.PlayService;
import jakarta.servlet.http.HttpSession;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@Controller
@RequestMapping("/api/levels")
public class LevelsController {

    private final CompleteService completeService;
    private final PlayService playService;

    @Autowired
    public LevelsController(CompleteService completeService, PlayService playService) {
        this.completeService = completeService;
        this.playService = playService;
    }

    @GetMapping("/completion-status")
    public ResponseEntity<List<Levels>> getLevelCompletionStatus(HttpSession session) {
        try {
            Integer userId = (Integer) session.getAttribute("userId");
            if (userId == null) {
                return new ResponseEntity<>(HttpStatus.UNAUTHORIZED);
            }

            List<Levels> levels = completeService.findByUserId(userId);
            return new ResponseEntity<>(levels, HttpStatus.OK);
        } catch (Exception e) {
            return new ResponseEntity<>(HttpStatus.INTERNAL_SERVER_ERROR);
        }
    }

    @PutMapping("/completion-status/{levelId}")
    public ResponseEntity<String> updateLevelCompletionStatus(HttpSession session, @PathVariable int levelId) {
        try {
            Integer userId = (Integer) session.getAttribute("userId"); // รับ userId จาก session
            if (userId == null) {
                return new ResponseEntity<>("User not logged in", HttpStatus.UNAUTHORIZED);
            }

            boolean isUpdated = completeService.updateStatus(userId, levelId, true);
            if (isUpdated) {
                return new ResponseEntity<>("Completion status updated", HttpStatus.OK);
            } else {
                return new ResponseEntity<>("Failed to update completion status", HttpStatus.BAD_REQUEST);
            }
        } catch (Exception e) {
            return new ResponseEntity<>("Error updating completion status", HttpStatus.INTERNAL_SERVER_ERROR);
        }
    }

    @GetMapping("/playTable")
    public ResponseEntity<List<Play>> getLevelCompletionStatusByUser(HttpSession session) {
        try {
            Integer userId = (Integer) session.getAttribute("userId");

            if (userId == null) {
                return new ResponseEntity<>(HttpStatus.UNAUTHORIZED);
            }

            // Fetch the completion status from the service using userId
            List<Play> playList = playService.findByUserId(userId);

            if (playList.isEmpty()) {
                return new ResponseEntity<>(HttpStatus.NOT_FOUND);
            }

            return new ResponseEntity<>(playList, HttpStatus.OK);
        } catch (Exception e) {
            return new ResponseEntity<>(HttpStatus.INTERNAL_SERVER_ERROR);
        }
    }
}
