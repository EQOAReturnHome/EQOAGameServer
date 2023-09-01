function  event_say(choice)
diagOptions = {}
    npcDialogue = "Ya wanna make a deal or not? I don�t �ave all day ya know!"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end