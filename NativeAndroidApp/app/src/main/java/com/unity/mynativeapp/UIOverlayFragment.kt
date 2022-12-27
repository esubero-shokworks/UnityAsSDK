package com.unity.mynativeapp

import android.app.Fragment
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup

import com.unity.mynativeapp.databinding.FragmentUIOverlayBinding

class UIOverlayFragment : Fragment() {

    private lateinit var binding: FragmentUIOverlayBinding
    private var mUnity: UnityCommunication? = null

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        binding = FragmentUIOverlayBinding.inflate(inflater, container, false)
        return binding.root
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)
        binding.btnShowMain.setOnClickListener {
            mUnity?.showMain()
        }
        binding.btnSendMsg.setOnClickListener {
            mUnity?.sendMsg()
        }
        binding.btnUnload.setOnClickListener {
            mUnity?.unload()
        }
        binding.btnFinish.setOnClickListener {
            mUnity?.finished()
        }
    }


    fun setUnityPlayer(unity: UnityCommunication) {
        mUnity = unity
    }

}

interface UnityCommunication {
    fun showMain()
    fun sendMsg()
    fun unload()
    fun finished()
}