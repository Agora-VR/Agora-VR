
<p align="center">
<a href="https://github.com/Agora-VR"><img src="Image Assets\AgoraLogo-Transparent.png" height="200px"></a>
</p>

#
<p align="center">
<a href="https://github.com/Agora-VR/Agora-VR/releases" alt="Releases">
    <img src="https://img.shields.io/github/v/release/Agora-VR/Agora-VR?include_prereleases" /></a>

<a href="https://github.com/Agora-VR/Agora-VR/graphs/traffic" alt="Downloads">
    <img src="https://img.shields.io/github/downloads/Agora-VR/Agora-VR/total" /></a>

<a href="https://github.com/Agora-VR/Agora-VR/pulse" alt="Repo Size">
    <img src="https://img.shields.io/github/repo-size/Agora-VR/Agora-VR" /></a>

<a href="https://developer.oculus.com/quest/" alt="Platform">
    <img src="https://img.shields.io/static/v1?logo=oculus&label=platform&message=Oculus%20Quest&color=9517A9" /></a>

<a href="https://unity3d.com/unity/whats-new/2019.3.1" alt="Unity Version">
    <img src="https://img.shields.io/static/v1?logo=unity&label=unity%20version&message=2019.3.1&color=202020" /></a>
</p>

# Agora VR
Our project focuses on the treatment of social anxiety and social phobias by utilizing VR technology for psychological and cognitive therapy. The goal is to allow patients who suffer from Agoraphobia or related disorders such as Schizoid personality disorder (SPD) to be able to overcome their anxiety in a controlled manner within the comfort of their own environment. By gradually exposing the patient to different immersive and interactive environments, it is possible to reduce their anxiety over time.

## What is this project about?
Agoraphobia is defined as “a type of anxiety disorder in which you fear and avoid places or situations that might cause you to panic and make you feel trapped, helpless or embarrassed”. Similar disorders include Avoidant personality disorder (AvPD), Schizoid personality disorder (SPD), and Schizotypal personality disorder (STPD), all of which are characterized by social anxiety or the lack of interest in social interaction.

People who have these disorders find it difficult or impossible to go out for help and receive treatment. In other cases, after the first few treatments, they can start to distrust the therapist or have fears of rejection. This is where our idea begins to take shape.

Our idea revolves around using the Oculus Quest, a standalone VR HMD that does not need to be connected to a computer and is not bounded by wires. This means that the patient can have access to a relatively inexpensive treatment option that they can use within the comfort of their own space. The usage of VR also allows the patient to be immersed within the environments that can dynamically change depending on the stage of treatment or severity of the disorder.

Using psychotherapy (a type of therapy that helps change a person’s behavioral and thinking patterns), exposure therapy (a type of therapy that exposes the person to the things that they are afraid of or avoid in a controlled way), and metacognitive interpersonal therapy (MIT), we can reduce the effects of agoraphobia and other social anxiety disorders of patients who use our system.

It is best to watch our video where we explain everything in full detail (https://youtu.be/PuKoxOJlpNI).

## Features
Below are very short descriptions of the features we have in this project. Again, for more details, refer to our video.

### Sessions
The patient will use the system for exposure therapy sessions. These sessions can be controlled by a state machine or input from the clincian overseeing the patients' progress.

### Meeting Room Scenario
The meeting room scenario is a small environment and serves as a starting point to ease the patient into the therapy treatment. After they become comfortable with the meeting room scenario, they will be escalted to the Auditorium scenario which is much larger with more people and distractions.

### Auditorium Scenario
The auditorium scenario is a large environment with a seat count of around 700. This is a more complicated scenario than the meeting room.

### Heart-rate & Stress Level Tracking
Heart rate and blood oxygenation levels are measured in real time and transmitted to our application via Bluetooth Low Energy. We store and send this data to the database after every session so the clinician can see the patient's reactions to stressful situations.

### Movement Tracking & Voice Recording
The patient's movements are tracked as well as their audio recorded. We run a speech to text algorithm to get a comparison between their actual speech and the script they are meant to follow.

### Patient/Clinician/Caretaker Web Portal
The web portal allows the patient or clincian to access their session data and review their performance. We display all of the data collected as well as the audio clip of the session.

# Group S20-61 Members
<table>
  <tr>
    <th width="20%">
        <a href="https://www.linkedin.com/in/dannguyen-ce/"><img src="Image Assets\profile_dan.png"></a>
    </th>
    <th width="20%">
        <a href="https://www.linkedin.com/in/ted-moseley-6646b1192/"><img src="Image Assets\profile_ted.png"></a>
    </th>
    <th width="20%">
        <a href="https://github.com/MichaelTruongZ"><img src="Image Assets\profile_mike.png"></a>
    </th>
    <th width="20%">
        <a href="https://github.com/AryehNess"><img src="Image Assets\profile_aryeh.png"></a>
    </th>
    <th width="20%">
        <a href="https://www.linkedin.com/in/grigore-burdea-phd-9a028340/"><img src="Image Assets\profile_burdea.png"></a>
    </th>
  </tr>
  <tr>
    <th><a href="https://www.linkedin.com/in/dannguyen-ce/">Daniel Nguyen</a></th>
    <th><a href="https://www.linkedin.com/in/ted-moseley-6646b1192/">Ted Moseley</a></th>
    <th><a href="https://github.com/MichaelTruongZ">Michael Truong</a></th>
    <th><a href="https://github.com/AryehNess">Aryeh Ness</a></th>
    <th><a href="https://www.linkedin.com/in/grigore-burdea-phd-9a028340/">Dr. Grigore Burdea</a></th>
  </tr>
</table>

### Members' Contributions
>*All members primarily contributed based on their strengths but also assisted other team members in other areas when needed.*

[Daniel Nguyen](https://www.linkedin.com/in/dannguyen-ce/)
 - Art, Graphic Design, & Presentation
 - Unity Development Lead
 - Oculus Quest Integration & Testing
 - 3D Modeling Lead (Auditorium)
 - Character Animation
 - BLE HRM Integration & Build

[Ted Moseley](https://www.linkedin.com/in/ted-moseley-6646b1192/)
 - Backend & Database Lead
 - Website Frontend & Access Portal
 - Data Graphing & Display
 - BLE HRM Software
 - Data Collection
 - Video Editing

[Michael Truong](https://github.com/MichaelTruongZ)
 - Head & Hand Tracking
 - Audio Recording
 - Speech-To-Text Integration

[Aryeh Ness](https://github.com/AryehNess)
 - Therapy Research Lead
 - Research Protocol Lead
 - 3D Modeling (Meeting Room)


### Attributions
Special thanks to:
 - Bensound.com for royalty free music
 - Elin Höhler for Oculus Home scene


```
Rutgers University - New Brunswick
Electrical and Computer Engineering Department
Capstone Design - Dr. Hana Godrich
Advisor: Dr. Grigore Burdea
Spring 2020
```
