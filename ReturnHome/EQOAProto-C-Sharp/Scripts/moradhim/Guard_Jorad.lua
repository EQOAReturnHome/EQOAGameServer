function  event_say(choice)
diagOptions = {}
    npcDialogue = "Don't go causin' a ruckus.. without invitin' me first! Har har har!"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end