function  event_say(choice)
diagOptions = {}
    npcDialogue = "Yes, yes, I'm sure you've something important to say but I don't have time to listen.  I am way too busy."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end