function  event_say(choice)
diagOptions = {}
    npcDialogue = "As I get older, it gets more difficult to hold this post. But I continue to do so out of honor and love of this glade."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end