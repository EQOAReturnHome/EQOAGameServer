function  event_say(choice)
diagOptions = {}
    npcDialogue = "I might not be the greatest alchemist alive but I have mastered the creation of yellowjack water. The elves from Fayspires come for it, once they are seasoned enough to understand it."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end