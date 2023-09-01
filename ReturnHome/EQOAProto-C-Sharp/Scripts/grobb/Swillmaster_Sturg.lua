function  event_say(choice)
diagOptions = {}
    npcDialogue = "Sturg make starcrab grog. Trolls drink it all at night and Sturg need lots more starcrabs stocked during the day. Sturg struggle to keep up."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end