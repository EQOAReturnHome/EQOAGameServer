function  event_say(choice)
diagOptions = {}
    npcDialogue = "Yes, yes, I'm sure you have so much to say but it will have to wait. I am so pressed for time."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end