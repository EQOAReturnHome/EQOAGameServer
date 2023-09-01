function  event_say(choice)
diagOptions = {}
    npcDialogue = "Have a bet to settle? A friendly duel? Maybe a grudge to settle between two rivals? Well step onto this arena and challenge someone to a duel!!"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end