function  event_say(choice)
diagOptions = {}
    npcDialogue = "Our warriors are useless without a properly made sword. Our watchman smithies can forge an effective blade, even for a warrior of our elevation."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end