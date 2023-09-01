function  event_say(choice)
diagOptions = {}
    npcDialogue = "Have you traveled out west?  If you follow the path leaving Highpass to the west, you will eventually reach Darvar Manor in the heart of Jared's Blight."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end