# AI Usage Disclosure – UqsBlog Final Project

## Overview
This document outlines how AI was used to support the development of the UqsBlog Software Quality Assurance (SQA) final project. The intent is to provide transparency regarding the role of AI in supporting analysis, design, and implementation activities.

## Tools Used
- ChatGPT (OpenAI)

## Scope of AI Assistance

AI was used as a supporting tool, not as a replacement for understanding or decision-making. All outputs were reviewed, validated, and adapted to meet project requirements.

### 1. Test Strategy Development
AI was used to:
- Help structure the Integrated Test Strategy document
- Refine wording for:
  - Goals
  - Quality attributes
  - Risk analysis
  - Test effectiveness statements
- Provide examples of industry-standard practices (e.g., CI/CD, BDD, mutation testing)

All final content was reviewed and aligned with course materials and project-specific requirements.

---

### 2. Unit Test Design (xUnit + NSubstitute)
AI assisted with:
- Checking test case structures follow AAA (Arrange, Act, Assert)
- Checking coverage against business rules:
  - BR1: Author must exist
  - BR2: Author must not be locked
  - BR3: Valid post creation etc...
- Recommending edge cases (null, boundary values, invalid inputs)

All tests were implemented, verified, and debugged manually.

---

### 3. BDD Test Development (Reqnroll / Gherkin)
AI was used to:
- Aid configuration and resolve Reqnroll/dotnet compatability issues
- Refine Gherkin scenarios for clarity and conciseness 
- Ensure alignment between:
  - BDD scenarios
  - Unit tests
  - Business rules
  - Mapping BDD to domain logic

BDD tests were executed and validated using `dotnet test`.

---

### 4. Risk Analysis & Test Design Alignment
AI supported:
- Identifying gaps in the risk table 
- Checking alignment of business rules with risks
- Ensuring traceability between:
  - Risks
  - Test cases
  - Business rules

All risks and mitigations were reviewed and tailored to the UqsBlog system.

---

### 5. CI/CD and Tooling

All pipeline configurations were implemented and validated manually.

---

### 6. Mutation Testing (Stryker.NET)
AI was used to:
- Aid setup and configuration
- Interpret mutation testing results
- Suggest improvements to increase mutation score

Final conclusions regarding effectiveness were based on observed results.

---

## Limitations of AI Usage
- AI-generated suggestions were not blindly accepted
- Some AI recommendations were incomplete or incorrect and required correction
- Domain-specific decisions (e.g., business rule enforcement) were made independently

## Academic Integrity Statement
All work submitted reflects my own understanding and effort. AI was used strictly as a learning aid and productivity tool, similar to documentation or reference materials. Final decisions, implementations, and validations were performed independently.

## Summary
AI was leveraged to:
- Improve clarity
- Accelerate development
- Validate understanding
- Troubleshoot 
- Check alignment

All critical thinking, testing decisions, and final implementations remain our own.
