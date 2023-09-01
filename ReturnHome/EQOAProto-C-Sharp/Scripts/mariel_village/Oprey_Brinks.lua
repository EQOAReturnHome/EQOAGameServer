function  event_say(choice)
diagOptions = {}
    npcDialogue = "This corner of Tunaria has it's own challenges, but we've made an alliance with the humans to the south. If our village comes under attack, we can summon the militia at The Lost Watch on the coast."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end